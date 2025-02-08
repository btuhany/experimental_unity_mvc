using System;

namespace Batuhan.Core.MVC
{
    public abstract class MVCEntityBase
        <TModel,
        TView,
        TController> : IInitializable

        where TModel : IModel
        where TView : IView
        where TController : IController
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public abstract void Initialize();

        protected bool _isInitialized;
    }
}
