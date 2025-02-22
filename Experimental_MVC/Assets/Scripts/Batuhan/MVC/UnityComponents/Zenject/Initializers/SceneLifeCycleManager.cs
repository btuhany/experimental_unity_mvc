using Batuhan.MVC.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    /// <summary>
    /// Every scene should contain this object's instance.
    /// Every scene context should install the dependencies.
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
            for (int i = 0; i < _sceneReferences.Count; i++)
            {
                _sceneReferences[i].OnAwakeCallback();
            }
        }
        private void HandleSceneReferencesOnStartCallbacks()
        {
            for (int i = 0; i < _sceneEntryPoints.Count; i++)
            {
                _sceneEntryPoints[i].OnStartCallback();
            }
        }
        private void HandleSceneReferencesOnDestroyCallbacks()
        {
            for (int i = 0; i < _sceneReferences.Count; i++)
            {
                _sceneReferences[i].OnDestroyCallback();
            }
        }
        private void HandleReferencesToAddAppLifeCycle()
        {
            for (int i = 0; i < _referencesToAddAppLifeCycle.Count; i++)
            {
                var referenceToAdd = _referencesToAddAppLifeCycle[i];
                _appReferenceManager.AddToAppLifeCycle(referenceToAdd);
            }
            _referencesToAddAppLifeCycle.Clear();
        }
    }
}
