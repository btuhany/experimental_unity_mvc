using Batuhan.MVC.Base;
using SnakeExample.Entities.NewEntity;
namespace SnakeExample.Entities.GameManager 
{
    public class GameManagerController : BaseControllerWithModelAndContext<IGameManagerModel, IGameManagerContext>
    {
        public GameManagerController(IGameManagerModel model, IGameManagerContext context) : base(model, context)
        {
            model.GameState.Value = GameState.GameOver;
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public void OnAwakeCallback()
        {
        }

        public void OnDestroyCallback()
        {
        }
    }
}