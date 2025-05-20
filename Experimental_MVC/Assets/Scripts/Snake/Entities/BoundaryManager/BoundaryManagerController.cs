using Batuhan.EventBus;
using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using SnakeExample.Events;
using SnakeExample.Grid;
using Zenject;

namespace SnakeExample.Entities.ObstacleManager
{
    internal class BoundaryManagerController : BaseControllerWithViewOnly<BoundaryManagerView>, ISceneLifeCycleManaged
    {
        [Inject] private IGridViewHelper _gridView;
        [Inject] private IEventBus<GameEvent> _eventBus;
        public BoundaryManagerController(BoundaryManagerView view) : base(view)
        {
        }

        public void OnAwakeCallback()
        {
            _eventBus.Subscribe<SceneInitializationEvent>(OnSceneInitialization);
        }
        public void OnDestroyCallback()
        {
            _eventBus.Unsubscribe<SceneInitializationEvent>(OnSceneInitialization);
        }

        private void OnSceneInitialization(SceneInitializationEvent obj)
        {
            var maxPos = _gridView.GetMaxBoundaryPos();
            var minPos = _gridView.GetMinBoundaryPos();
            var cellSize = _gridView.CellSize;
            _view.ConstructBoundaryObstacles(maxPos, minPos, cellSize);
        }
    }
}
