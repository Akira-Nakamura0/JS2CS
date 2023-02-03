using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using JS2CS.Net;

namespace JS2CS
{
  internal class HttpResponse : HttpBase
  {
    #region Private Fields

    private int    _code;
    private string _reason;

    #endregion

    #region Private Constructors

    private HttpResponse (
      int code, string reason, Version version, NameValueCollection headers
    )
      : base (version, headers)
    {
      _code = code;
      _reason = reason;
    }

    #endregion

    #region Internal Constructors

    internal HttpResponse (int code)
      : this (code, code.GetStatusDescription ())
    {
    }

    internal HttpResponse (HttpStatusCode code)
      : this ((int) code)
    {
    }

    internal HttpResponse (int code, string reason)
      : this (
          code,
          reason,
          HttpVersion.Version11,
          new NameValueCollection ()
        )
    {
      Headers["Server"] = "websocket-sharp/1.0";
    }

    internal HttpResponse (HttpStatusCode code, string reason)
      : this ((int) code, reason)
    {
    }

    #endregion

    #region Internal Properties

    internal string StatusLine {
      get {
        return _reason != null
               ? String.Format (
                   "HTTP/{0} {1} {2}{3}", ProtocolVersion, _code, _reason, CrLf
                 )
               : String.Format (
                   "HTTP/{0} {1}{2}", ProtocolVersion, _code, CrLf
                 );
      }
    }

    #endregion

    #region Public Properties

    public bool CloseConnection {
      get {
        var compType = StringComparison.OrdinalIgnoreCase;

        return Headers.Contains ("Connection", "close", compType);
      }
    }

    public CookieCollection Cookies {
      get {
        return Headers.GetCookies (true);
      }
    }

    public bool IsProxyAuthenticationRequired {
      get {
        return _code == 407;
      }
    }

    public bool IsRedirect {
      get {
        return _code == 301 || _code == 302;
      }
    }

    public bool IsSuccess {
      get {
        return _code >= 200 && _code <= 299;
      }
    }

    public bool IsUnauthorized {
      get {
        return _code == 401;
      }
    }

    public bool IsWebSocketResponse {
      get {
        return ProtocolVersion > HttpVersion.Version10
               && _code == 101
               && Headers.Upgrades ("websocket");
      }
    }

    public override string MessageHeader {
      get {
        return StatusLine + HeaderSection;
      }
    }

    public string Reason {
      get {
        return _reason;
      }
    }

    public int StatusCode {
      get {
        return _code;
      }
    }

    #endregion

    #region Internal Methods

    internal static HttpResponse CreateCloseResponse (HttpStatusCode code)
    {
      var ret = new HttpResponse (code);

      ret.Headers["Connection"] = "close";

      return ret;
    }

    internal static HttpResponse CreateUnauthorizedResponse (string challenge)
    {
      var ret = new HttpResponse (HttpStatusCode.Unauthorized);

      ret.Headers["WWW-Authenticate"] = challenge;

      return ret;
    }

    internal static HttpResponse CreateWebSocketHandshakeResponse ()
    {
      var ret = new HttpResponse (HttpStatusCode.SwitchingProtocols);

      var headers = ret.Headers;

      headers["Upgrade"] = "websocket";
      headers["Connection"] = "Upgrade";

      return ret;
    }

    internal static HttpResponse Parse (string[] messageHeader)
    {
      var len = messageHeader.Length;

      if (len == 0) {
        var msg = "An empty response header.";

        throw new ArgumentException (msg);
      }

      var slParts = messageHeader[0].Split (new[] { ' ' }, 3);
      var plen = slParts.Length;

      if (plen < 2) {
        var msg = "It includes an invalid status line.";

        throw new ArgumentException (msg);
      }

      var code = slParts[1].ToInt32 ();
      var reason = plen == 3 ? slParts[2] : null;
      var ver = slParts[0].Substring (5).ToVersion ();

      var headers = new WebHeaderCollection ();

      for (var i = 1; i < len; i++)
        headers.InternalSet (messageHeader[i], true);

      return new HttpResponse (code, reason, ver, headers);
    }

    internal static HttpResponse ReadResponse (
      Stream stream, int millisecondsTimeout
    )
    {
      return Read<HttpResponse> (stream, Parse, millisecondsTimeout);
    }

    #endregion

    #region Public Methods

    public void SetCookies (CookieCollection cookies)
    {
      if (cookies == null || cookies.Count == 0)
        return;

      var headers = Headers;

      foreach (var cookie in cookies.Sorted) {
        var val = cookie.ToResponseString ();

        headers.Add ("Set-Cookie", val);
      }
    }

    #endregion
  }
}
