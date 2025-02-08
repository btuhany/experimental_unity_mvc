using Unity.Android.Gradle.Manifest;

namespace Batuhan.Core.MVC.Base
{
    public abstract class BaseEntity
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

        protected TController _controller;
        protected TModel _model;
        protected TView _view;
    }
}
