using System;

namespace JS2CS.Server
{
  /// <summary>
  /// Exposes the access to the information in a WebSocket session.
  /// </summary>
  public interface IWebSocketSession
  {
    #region Properties

    /// <summary>
    /// Gets the unique ID of the session.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> that represents the unique ID of the session.
    /// </value>
    string ID { get; }

    /// <summary>
    /// Gets the time that the session has started.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> that represents the time that the session
    /// has started.
    /// </value>
    DateTime StartTime { get; }

    /// <summary>
    /// Gets the WebSocket interface for the session.
    /// </summary>
    /// <value>
    /// A <see cref="JS2CS.WebSocket"/> that represents the interface.
    /// </value>
    WebSocket WebSocket { get; }

    #endregion
  }
}
