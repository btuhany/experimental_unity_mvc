using Batuhan.MVC.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TimeCounter.Entities.Initializer
{
    //TODOBY move to core
    public abstract class SceneInitializer : MonoBehaviour
    {
        private SceneReferenceManager _refHolder;

        [Inject]
        public void Construct(SceneReferenceManager refHolder)
        {
            _refHolder = refHolder;
        }

        private void Awake()
        {
            _refHolder.HandleOnAwake();
        }
        private void Start()
        {
            _refHolder.HandleOnStart();
        }
        private void OnDestroy()
        {
            _refHolder.HandleOnDestroy();
        }
    }
    public class SceneReferenceManager
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
                _lifeCycleHandlers[i].Initialize();
            }
        }
        public virtual void HandleOnStart()
        {
            for (int i = 0; i < _entryPoints.Count; i++)
            {
                _entryPoints[i].Start();
            }
        }
        public virtual void HandleOnDestroy()
        {
            for (int i = 0; i < _lifeCycleHandlers.Count; i++)
            {
                _lifeCycleHandlers[i].Dispose();
            }
        }
    }
}
