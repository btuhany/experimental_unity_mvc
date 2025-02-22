using System;

namespace Batuhan.MVC.Core
{
    public delegate void AppLifeCycleManagedDelegate(IAppLifeCycleManaged appLifeCyleManaged);
    public interface IAppLifeCycleManaged : IDisposable
    {
        public AppLifeCycleManagedDelegate RemoveFromAppLifeCycleAction { get; set; }
        void Initialize();
    }
}
