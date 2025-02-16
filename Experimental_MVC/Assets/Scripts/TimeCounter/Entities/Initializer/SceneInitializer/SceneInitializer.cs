using UnityEngine;
using Zenject;

namespace TimeCounter.Entities.Initializer
{
    public abstract class SceneInitializer<TReferenceHolder> : MonoBehaviour where TReferenceHolder : SceneReferenceManager, new()
    {
        [Inject]
        private TReferenceHolder _refHolder;
        private void Awake()
        {
            _refHolder.HandleOnAwake();
        }
        private void OnDestroy()
        {
            _refHolder.HandleOnDestroy();
        }
    }
    public abstract class SceneReferenceManager
    {
        public abstract void HandleOnAwake();
        public abstract void HandleOnDestroy();
    }
}
