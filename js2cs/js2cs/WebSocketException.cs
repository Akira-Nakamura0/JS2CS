using System;

namespace JS2CS
{
  /// <summary>
  /// The exception that is thrown when a fatal error occurs in
  /// the WebSocket communication.
  /// </summary>
  public class WebSocketException : Exception
  {
    #region Private Fields

    private ushort _code;

    #endregion

    #region Private Constructors

    private WebSocketException (
      ushort code, string message, Exception innerException
    )
      : base (message ?? code.GetErrorMessage (), innerException)
    {
      _code = code;
    }

    #endregion

    #region Internal Constructors

    internal WebSocketException ()
      : this (CloseStatusCode.Abnormal, null, null)
    {
    }

    internal WebSocketException (Exception innerException)
      : this (CloseStatusCode.Abnormal, null, innerException)
    {
    }

    internal WebSocketException (string message)
      : this (CloseStatusCode.Abnormal, message, null)
    {
    }

    internal WebSocketException (CloseStatusCode code)
      : this (code, null, null)
    {
    }

    internal WebSocketException (string message, Exception innerException)
      : this (CloseStatusCode.Abnormal, message, innerException)
    {
    }

    internal WebSocketException (CloseStatusCode code, Exception innerException)
      : this (code, null, innerException)
    {
    }

    internal WebSocketException (CloseStatusCode code, string message)
      : this (code, message, null)
    {
    }

    internal WebSocketException (
      CloseStatusCode code, string message, Exception innerException
    )
      : this ((ushort) code, message, innerException)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the status code indicating the cause of the exception.
    /// </summary>
    /// <value>
    ///   <para>
    ///   A <see cref="ushort"/> that represents the status code indicating
    ///   the cause of the exception.
    ///   </para>
    ///   <para>
    ///   It is one of the status codes for the WebSocket connection close.
    ///   </para>
    /// </value>
    public ushort Code {
      get {
        return _code;
      }
    }

    #endregion
  }
}
