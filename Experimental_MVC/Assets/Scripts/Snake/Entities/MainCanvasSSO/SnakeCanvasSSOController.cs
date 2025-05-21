using System;
using Batuhan.EventBus;
using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using SnakeExample.Entities.GameManager;
using SnakeExample.Events;
using Zenject;

namespace ExperimentalMVC.SnakeExample.Entities.MainCanvasSSO
{
    public class SnakeCanvasSSOController : BaseControllerWithViewOnly<SnakeCanvasSSOView>, ISceneLifeCycleManaged 
    {
        [Inject] private IEventBus<GameEvent> _eventBus;
        public SnakeCanvasSSOController(SnakeCanvasSSOView view) : base(view)
        {
        }

        public void OnAwakeCallback()
        {
            _eventBus.Subscribe<GameStateChanged>(OnGameStateChanged);
            _view.ShowPressAny();
        }

        public void OnDestroyCallback()
        {
            _eventBus.Unsubscribe<GameStateChanged>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChanged obj)
        {
            var state = obj.NewState;
            switch (state)
            {
                case GameState.PressAny:
                    _view.ShowPressAny();
                    break;
                case GameState.Started:
                    _view.ShowStartAsync();
                    break;
                case GameState.GameOver:
                    _view.ShowGameOver();
                    break;
                case GameState.RestartDelay:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}