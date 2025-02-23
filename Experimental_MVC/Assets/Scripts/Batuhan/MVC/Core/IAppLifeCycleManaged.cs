using Batuhan.MVC.UnityComponents.Zenject;
using System;

namespace Batuhan.MVC.Core
{
    public delegate void AppLifeCycleManagedDelegate(IAppLifeCycleManaged appLifeCyleManaged);
    public interface IAppLifeCycleManaged : IDisposable
    {
        public AppLifeCycleManagedDelegate RemoveFromAppLifeCycleAction { get; set; }
        void Initialize();
        ReferenceAllocationMode AllocationMode { get; }
        Type AllocationRegistrationType { get; }
    }
}
