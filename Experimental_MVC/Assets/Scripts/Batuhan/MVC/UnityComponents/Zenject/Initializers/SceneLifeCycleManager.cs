using Batuhan.MVC.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    /// <summary>
    /// Every scene should contain this object's instance.
    /// Every scene context should install the dependencies.
    /// Should be executed after all objects in the scene are executed. (because of view mono behaviours)
    /// </summary>
    public class SceneLifeCycleManager : MonoBehaviour
    {
        /// <summary>
        /// Objects in scene to invoke callbacks.
        /// </summary>
        [Inject]
        private List<ISceneLifeCycleManaged> _sceneReferences;
        /// <summary>
        /// Usually there should be only one entry point. But for customization i will leave it with list.
        /// </summary>
        [Inject]
        private List<IEntryPoint> _sceneEntryPoints;
        /// <summary>
        /// Objects to add app reference manager at start of the scene.
        /// </summary>
        [Inject]
        private List<IAppLifeCycleManaged> _referencesToAddAppLifeCycle;
        /// <summary>
        /// Global app reference manager that persists between scene transitions.
        /// </summary>
        [Inject]
        private IAppReferenceManager _appReferenceManager;

#if UNITY_EDITOR
        public List<IAppLifeCycleManaged> AppReferencesAddedToAppLifeCycle;
#endif
        private void Awake()
        {
            HandleSceneReferencesOnAwakeCallbacks();
        }
        private void Start()
        {
            HandleSceneReferencesOnStartCallbacks();
            HandleReferencesToAddAppLifeCycle();
        }
        private void OnDestroy()
        {
            HandleSceneReferencesOnDestroyCallbacks();
        }
        private void OnApplicationQuit()
        {
            _appReferenceManager.OnApplicationQuitCallback();
        }
        private void HandleSceneReferencesOnAwakeCallbacks()
        {
            if (_sceneReferences is null)
                return;

            for (int i = 0; i < _sceneReferences.Count; i++)
            {
                _sceneReferences[i].OnAwakeCallback();
            }
        }
        private void HandleSceneReferencesOnStartCallbacks()
        {
            if (_sceneEntryPoints is null)
                return;

            for (int i = 0; i < _sceneEntryPoints.Count; i++)
            {
                _sceneEntryPoints[i].OnStartCallback();
            }
        }
        private void HandleSceneReferencesOnDestroyCallbacks()
        {
            if (_sceneReferences is null)
                return;

            for (int i = 0; i < _sceneReferences.Count; i++)
            {
                _sceneReferences[i].OnDestroyCallback();
            }
        }
        private void HandleReferencesToAddAppLifeCycle()
        {
            if (_referencesToAddAppLifeCycle is null)
                return;

#if UNITY_EDITOR
            AppReferencesAddedToAppLifeCycle = new List<IAppLifeCycleManaged>();
#endif
            for (int i = 0; i < _referencesToAddAppLifeCycle.Count; i++)
            {
                var referenceToAdd = _referencesToAddAppLifeCycle[i];
                bool isAdded = _appReferenceManager.AddToAppLifeCycle(referenceToAdd);
#if UNITY_EDITOR
                if (isAdded)
                    AppReferencesAddedToAppLifeCycle.Add(referenceToAdd);
#endif
            }
            _referencesToAddAppLifeCycle.Clear();
        }
    }
}
