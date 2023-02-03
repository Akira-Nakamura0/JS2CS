using System;
using System.Collections.Specialized;
using System.Text;

namespace JS2CS.Net
{
  internal sealed class QueryStringCollection : NameValueCollection
  {
    #region Public Constructors

    public QueryStringCollection ()
    {
    }

    public QueryStringCollection (int capacity)
      : base (capacity)
    {
    }

    #endregion

    #region Public Methods

    public static QueryStringCollection Parse (string query)
    {
      return Parse (query, Encoding.UTF8);
    }

    public static QueryStringCollection Parse (string query, Encoding encoding)
    {
      if (query == null)
        return new QueryStringCollection (1);

      if (query.Length == 0)
        return new QueryStringCollection (1);

      if (query == "?")
        return new QueryStringCollection (1);

      if (query[0] == '?')
        query = query.Substring (1);

      if (encoding == null)
        encoding = Encoding.UTF8;

      var ret = new QueryStringCollection ();

      foreach (var component in query.Split ('&')) {
        var len = component.Length;

        if (len == 0)
          continue;

        if (component == "=")
          continue;

        string name = null;
        string val = null;

        var idx = component.IndexOf ('=');

        if (idx < 0) {
          val = component.UrlDecode (encoding);
        }
        else if (idx == 0) {
          val = component.Substring (1).UrlDecode (encoding);
        }
        else {
          name = component.Substring (0, idx).UrlDecode (encoding);

          var start = idx + 1;
          val = start < len
                ? component.Substring (start).UrlDecode (encoding)
                : String.Empty;
        }

        ret.Add (name, val);
      }

      return ret;
    }

    public override string ToString ()
    {
      var buff = new StringBuilder ();

      foreach (var key in AllKeys)
        buff.AppendFormat ("{0}={1}&", key, this[key]);

      if (buff.Length > 0)
        buff.Length--;

      return buff.ToString ();
    }

    #endregion
  }
}
