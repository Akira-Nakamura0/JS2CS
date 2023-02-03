using System;

namespace JS2CS.Server
{
  internal class WebSocketServiceHost<TBehavior> : WebSocketServiceHost
    where TBehavior : WebSocketBehavior, new ()
  {
    #region Private Fields

    private Func<TBehavior> _creator;

    #endregion

    #region Internal Constructors

    internal WebSocketServiceHost (
      string path, Action<TBehavior> initializer, Logger log
    )
      : base (path, log)
    {
      _creator = createSessionCreator (initializer);
    }

    #endregion

    #region Public Properties

    public override Type BehaviorType {
      get {
        return typeof (TBehavior);
      }
    }

    #endregion

    #region Private Methods

    private static Func<TBehavior> createSessionCreator (
      Action<TBehavior> initializer
    )
    {
      if (initializer == null)
        return () => new TBehavior ();

      return () => {
               var ret = new TBehavior ();

               initializer (ret);

               return ret;
             };
    }

    #endregion

    #region Protected Methods

    protected override WebSocketBehavior CreateSession ()
    {
      return _creator ();
    }

    #endregion
  }
}
