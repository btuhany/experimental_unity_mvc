using Batuhan.MVC.Core;
using System.Collections.Generic;

namespace Batuhan.MVC.UnityComponents.Core
{
    public interface ISceneReferenceManager
    {
        public List<ILifeCycleHandler> LifeCycleHandlers { get; }
        public List<IEntryPoint> EntryPoints { get; }
        void HandleOnAwake();
        void HandleOnStart();
        void HandleOnDestroy();
    }
}