
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace JS2CS.Net
{
  /// <summary>
  /// The exception that is thrown when a <see cref="Cookie"/> gets an error.
  /// </summary>
  [Serializable]
  public class CookieException : FormatException, ISerializable
  {
    #region Internal Constructors

    internal CookieException (string message)
      : base (message)
    {
    }

    internal CookieException (string message, Exception innerException)
      : base (message, innerException)
    {
    }

    #endregion

    #region Protected Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CookieException"/> class
    /// with the serialized data.
    /// </summary>
    /// <param name="serializationInfo">
    /// A <see cref="SerializationInfo"/> that holds the serialized object data.
    /// </param>
    /// <param name="streamingContext">
    /// A <see cref="StreamingContext"/> that specifies the source for
    /// the deserialization.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="serializationInfo"/> is <see langword="null"/>.
    /// </exception>
    protected CookieException (
      SerializationInfo serializationInfo, StreamingContext streamingContext
    )
      : base (serializationInfo, streamingContext)
    {
    }

    #endregion

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CookieException"/> class.
    /// </summary>
    public CookieException ()
      : base ()
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> instance with
    /// the data needed to serialize the current instance.
    /// </summary>
    /// <param name="serializationInfo">
    /// A <see cref="SerializationInfo"/> that holds the serialized object data.
    /// </param>
    /// <param name="streamingContext">
    /// A <see cref="StreamingContext"/> that specifies the destination for
    /// the serialization.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="serializationInfo"/> is <see langword="null"/>.
    /// </exception>
    [
      SecurityPermission (
        SecurityAction.LinkDemand,
        Flags = SecurityPermissionFlag.SerializationFormatter
      )
    ]
    public override void GetObjectData (
      SerializationInfo serializationInfo, StreamingContext streamingContext
    )
    {
      base.GetObjectData (serializationInfo, streamingContext);
    }

    #endregion

    #region Explicit Interface Implementation

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> instance with
    /// the data needed to serialize the current instance.
    /// </summary>
    /// <param name="serializationInfo">
    /// A <see cref="SerializationInfo"/> that holds the serialized object data.
    /// </param>
    /// <param name="streamingContext">
    /// A <see cref="StreamingContext"/> that specifies the destination for
    /// the serialization.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="serializationInfo"/> is <see langword="null"/>.
    /// </exception>
    [
      SecurityPermission (
        SecurityAction.LinkDemand,
        Flags = SecurityPermissionFlag.SerializationFormatter,
        SerializationFormatter = true
      )
    ]
    void ISerializable.GetObjectData (
      SerializationInfo serializationInfo, StreamingContext streamingContext
    )
    {
      base.GetObjectData (serializationInfo, streamingContext);
    }

    #endregion
  }
}
