using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Core;
using System.Collections.Generic;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public class SceneReferenceManager : ISceneReferenceManager
    {
        [Inject]
        private List<ILifeCycleHandler> _lifeCycleHandlers;

        /// <summary>
        /// Usually there should be only one entry point. But for customization i will leave it with list.
        /// </summary>
        [Inject]
        private List<IEntryPoint> _entryPoints;


        public virtual void HandleOnAwake()
        {
            for (int i = 0; i < _lifeCycleHandlers.Count; i++)
            {
                _lifeCycleHandlers[i].OnAwakeCallback();
            }
        }
        public virtual void HandleOnStart()
        {
            for (int i = 0; i < _entryPoints.Count; i++)
            {
                _entryPoints[i].OnStartCallback();
            }
        }
        public virtual void HandleOnDestroy()
        {
            for (int i = 0; i < _lifeCycleHandlers.Count; i++)
            {
                _lifeCycleHandlers[i].OnDestroyCallback();
            }
        }

    }
}
