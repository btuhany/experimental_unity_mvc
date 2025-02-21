using System;

namespace Batuhan.MVC.Core
{
    public delegate void DestroyThis(IAppLifeCycleManaged appLifeCyleManaged);
    public interface IAppLifeCycleManaged : ILifeCycleManaged
    {
        public DestroyThis DestroyDelegate { get; set; } 
    }
}
