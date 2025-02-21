using System;
using Zenject;

namespace Batuhan.MVC.Core
{
    public delegate void DestroyThis(IAppLifeCycleManaged appLifeCyleManaged);
    public interface IAppLifeCycleManaged : IDisposable
    {
        public DestroyThis DestroyDelegate { get; set; }
        void Initialize();
    }
}
