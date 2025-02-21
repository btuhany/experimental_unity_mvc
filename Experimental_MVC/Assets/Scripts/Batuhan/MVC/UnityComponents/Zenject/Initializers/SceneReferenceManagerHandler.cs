using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public interface ISceneReferenceManagerHandler
    {
        ISceneReferenceManager ReferenceManager { get; }
    }
    //TODOBY not sure about the name
    public abstract class SceneReferenceManagerHandler : MonoBehaviour, ISceneReferenceManagerHandler
    {
        [Inject]
        private ISceneReferenceManager _referenceManager;

        public ISceneReferenceManager ReferenceManager => _referenceManager;

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
