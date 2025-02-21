using Batuhan.MVC.UnityComponents.Core;
using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public abstract class SceneReferencesInitializer : MonoBehaviour, ISceneInitializer
    {
        private SceneReferenceManager _referenceManager;

        public SceneReferenceManager ReferenceManager => _referenceManager;

        [Inject]
        public void Construct(SceneReferenceManager refHolder)
        {
            _referenceManager = refHolder;
        }

        private void Awake()
        {
            _referenceManager.HandleOnAwake();
        }
        private void Start()
        {
            _referenceManager.HandleOnStart();
        }
        private void OnDestroy()
        {
            _referenceManager.HandleOnDestroy();
        }
    }
}
