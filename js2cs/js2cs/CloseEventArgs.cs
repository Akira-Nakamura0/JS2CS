using System;

namespace JS2CS
{
  /// <summary>
  /// Represents the event data for the <see cref="WebSocket.OnClose"/> event.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///   The close event occurs when the WebSocket connection has been closed.
  ///   </para>
  ///   <para>
  ///   If you would like to get the reason for the connection close,
  ///   you should access the <see cref="Code"/> or <see cref="Reason"/>
  ///   property.
  ///   </para>
  /// </remarks>
  public class CloseEventArgs : EventArgs
  {
    #region Private Fields

    private bool        _clean;
    private PayloadData _payloadData;

    #endregion

    #region Internal Constructors

    internal CloseEventArgs (PayloadData payloadData, bool clean)
    {
      _payloadData = payloadData;
      _clean = clean;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the status code for the connection close.
    /// </summary>
    /// <value>
    ///   <para>
    ///   A <see cref="ushort"/> that represents the status code for
    ///   the connection close.
    ///   </para>
    ///   <para>
    ///   1005 (no status) if not present.
    ///   </para>
    /// </value>
    public ushort Code {
      get {
        return _payloadData.Code;
      }
    }

    /// <summary>
    /// Gets the reason for the connection close.
    /// </summary>
    /// <value>
    ///   <para>
    ///   A <see cref="string"/> that represents the reason for
    ///   the connection close.
    ///   </para>
    ///   <para>
    ///   An empty string if not present.
    ///   </para>
    /// </value>
    public string Reason {
      get {
        return _payloadData.Reason;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the connection has been closed cleanly.
    /// </summary>
    /// <value>
    /// <c>true</c> if the connection has been closed cleanly; otherwise,
    /// <c>false</c>.
    /// </value>
    public bool WasClean {
      get {
        return _clean;
      }
    }

    #endregion
  }
}
