using Batuhan.MVC.Base;
using Batuhan.MVC.Core;

namespace SnakeExample.Entities.ObstacleManager
{
    internal class ObstacleManagerController : BaseControllerWithViewAndContext<ObstacleManagerView, ObstacleManagerContext>, ISceneLifeCycleManaged
    {
        public ObstacleManagerController(ObstacleManagerView view, ObstacleManagerContext context) : base(view, context)
        {
        }

        public void OnAwakeCallback()
        {
            var maxPos = _context.GridView.GetMaxBoundaryPos();
            var minPos = _context.GridView.GetMinBoundaryPos();
            var cellSize = _context.GridView.CellSize;
            _view.ConstructBoundaryObstacles(maxPos, minPos, cellSize);
        }
        public void OnDestroyCallback()
        {
        }
    }
}
