using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using SnakeExample.Events;
namespace SnakeExample.Entities.GameManager 
{
    public class GameManagerController : BaseControllerWithModelAndContext<IGameManagerModel, IGameManagerContext>, ISceneLifeCycleManaged
    {
        public GameManagerController(IGameManagerModel model, IGameManagerContext context) : base(model, context)
        {
            _model.GameState.Subscribe(OnGameStateChanged);
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public void OnAwakeCallback()
        {
            _model.Initialize();
            _model.ChangeGameState(GameState.PressAny);
            UnityEngine.Debug.Log("Game manager controller awake!");
        }

        public void OnDestroyCallback()
        {
        }
        private void OnGameStateChanged(GameState state)
        {
            _context.EventGameBus.Publish(new GameStateChanged() { NewState = state});
        }
    }
}