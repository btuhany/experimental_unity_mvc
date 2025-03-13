using Batuhan.MVC.Base;
using Batuhan.MVC.Core;

namespace SnakeExample.Entities.Snake
{
    internal class SnakeController : BaseController<SnakeModel, SnakeView, SnakeContext>, ISceneLifeCycleManaged
    {
        public SnakeController(SnakeModel model, SnakeView view, SnakeContext context) : base(model, view, context)
        {
        }

        public void OnAwakeCallback()
        {
        }

        public void OnDestroyCallback()
        {
        }
    }
}