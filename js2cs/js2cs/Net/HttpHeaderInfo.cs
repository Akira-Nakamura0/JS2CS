using System;

namespace JS2CS.Net
{
  internal class HttpHeaderInfo
  {
    #region Private Fields

    private string         _headerName;
    private HttpHeaderType _headerType;

    #endregion

    #region Internal Constructors

    internal HttpHeaderInfo (string headerName, HttpHeaderType headerType)
    {
      _headerName = headerName;
      _headerType = headerType;
    }

    #endregion

    #region Internal Properties

    internal bool IsMultiValueInRequest {
      get {
        var headerType = _headerType & HttpHeaderType.MultiValueInRequest;

        return headerType == HttpHeaderType.MultiValueInRequest;
      }
    }

    internal bool IsMultiValueInResponse {
      get {
        var headerType = _headerType & HttpHeaderType.MultiValueInResponse;

        return headerType == HttpHeaderType.MultiValueInResponse;
      }
    }

    #endregion

    #region Public Properties

    public string HeaderName {
      get {
        return _headerName;
      }
    }

    public HttpHeaderType HeaderType {
      get {
        return _headerType;
      }
    }

    public bool IsRequest {
      get {
        var headerType = _headerType & HttpHeaderType.Request;

        return headerType == HttpHeaderType.Request;
      }
    }

    public bool IsResponse {
      get {
        var headerType = _headerType & HttpHeaderType.Response;

        return headerType == HttpHeaderType.Response;
      }
    }

    #endregion

    #region Public Methods

    public bool IsMultiValue (bool response)
    {
      var headerType = _headerType & HttpHeaderType.MultiValue;

      if (headerType != HttpHeaderType.MultiValue)
        return response ? IsMultiValueInResponse : IsMultiValueInRequest;

      return response ? IsResponse : IsRequest;
    }

    public bool IsRestricted (bool response)
    {
      var headerType = _headerType & HttpHeaderType.Restricted;

      if (headerType != HttpHeaderType.Restricted)
        return false;

      return response ? IsResponse : IsRequest;
    }

    #endregion
  }
}
