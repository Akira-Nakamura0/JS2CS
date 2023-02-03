using System;

namespace JS2CS
{
  /// <summary>
  /// Represents the event data for the <see cref="WebSocket.OnError"/> event.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///   The error event occurs when the <see cref="WebSocket"/> interface
  ///   gets an error.
  ///   </para>
  ///   <para>
  ///   If you would like to get the error message, you should access
  ///   the <see cref="ErrorEventArgs.Message"/> property.
  ///   </para>
  ///   <para>
  ///   If the error is due to an exception, you can get it by accessing
  ///   the <see cref="ErrorEventArgs.Exception"/> property.
  ///   </para>
  /// </remarks>
  public class ErrorEventArgs : EventArgs
  {
    #region Private Fields

    private Exception _exception;
    private string    _message;

    #endregion

    #region Internal Constructors

    internal ErrorEventArgs (string message)
      : this (message, null)
    {
    }

    internal ErrorEventArgs (string message, Exception exception)
    {
      _message = message;
      _exception = exception;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the exception that caused the error.
    /// </summary>
    /// <value>
    ///   <para>
    ///   An <see cref="System.Exception"/> instance that represents
    ///   the cause of the error.
    ///   </para>
    ///   <para>
    ///   <see langword="null"/> if not present.
    ///   </para>
    /// </value>
    public Exception Exception {
      get {
        return _exception;
      }
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> that represents the error message.
    /// </value>
    public string Message {
      get {
        return _message;
      }
    }

    #endregion
  }
}
