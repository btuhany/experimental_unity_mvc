using Batuhan.MVC.UnityComponents.Core;
using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public abstract class SceneInitializer : MonoBehaviour, ISceneInitializer
    {
        private SceneReferenceManager _referenceManager;

        public SceneReferenceManager ReferenceManager => throw new System.NotImplementedException();

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
