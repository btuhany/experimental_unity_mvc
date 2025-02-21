using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Core;
using System.Collections.Generic;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public class SceneReferenceManager : ISceneReferenceManager
    {
        [Inject]
        private List<ISceneLifeCycleManaged> _sceneLifeCycleReferences;

        /// <summary>
        /// Usually there should be only one entry point. But for customization i will leave it with list.
        /// </summary>
        [Inject]
        private List<IEntryPoint> _sceneEntryPoints;


        public virtual void HandleOnAwake()
        {
            for (int i = 0; i < _sceneLifeCycleReferences.Count; i++)
            {
                _sceneLifeCycleReferences[i].OnAwakeCallback();
            }
        }
        public virtual void HandleOnStart()
        {
            for (int i = 0; i < _sceneEntryPoints.Count; i++)
            {
                _sceneEntryPoints[i].OnStartCallback();
            }
        }
        public virtual void HandleOnDestroy()
        {
            for (int i = 0; i < _sceneLifeCycleReferences.Count; i++)
            {
                _sceneLifeCycleReferences[i].OnDestroyCallback();
            }
        }

    }
}
