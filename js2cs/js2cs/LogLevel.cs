using System;

namespace JS2CS
{
  /// <summary>
  /// Specifies the logging level.
  /// </summary>
  public enum LogLevel
  {
    /// <summary>
    /// Specifies the bottom logging level.
    /// </summary>
    Trace,
    /// <summary>
    /// Specifies the 2nd logging level from the bottom.
    /// </summary>
    Debug,
    /// <summary>
    /// Specifies the 3rd logging level from the bottom.
    /// </summary>
    Info,
    /// <summary>
    /// Specifies the 3rd logging level from the top.
    /// </summary>
    Warn,
    /// <summary>
    /// Specifies the 2nd logging level from the top.
    /// </summary>
    Error,
    /// <summary>
    /// Specifies the top logging level.
    /// </summary>
    Fatal,
    /// <summary>
    /// Specifies not to output logs.
    /// </summary>
    None
  }
}
