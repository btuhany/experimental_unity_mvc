using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using SnakeExample.Events;
using System;
using Cysharp.Threading.Tasks;
using SnakeExample.Config;
using Zenject;

namespace SnakeExample.Entities.GameManager 
{
    public class GameManagerController : BaseControllerWithModelAndContext<IGameManagerModel, IGameManagerContext>, ISceneLifeCycleManaged
    {
        [Inject] private GameConfigDataSO _configData;
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

            _context.EventGameBus.Subscribe<SnakeStoppedEvent>(OnSnakeStopped);
            _context.InputSource.OnPressAnyAction += OnInput;
            UnityEngine.Debug.Log("Game manager controller awake!");
        }

        private void OnSnakeStopped(SnakeStoppedEvent @event)
        {
            _model.ChangeGameState(GameState.GameOver);
            RestartAsync().Forget();
        }

        public void OnDestroyCallback()
        {
            _context.InputSource.OnPressAnyAction -= OnInput;
            _context.EventGameBus.Unsubscribe<SnakeStoppedEvent>(OnSnakeStopped);
        }
        private void OnInput()
        {
            if (_model.GameState.CurrentValue == GameState.PressAny)
            {
                _model.ChangeGameState(GameState.Started);
                UnityEngine.Debug.Log("STATE CHANGED!");
            }
        }
        private void OnGameStateChanged(GameState state)
        {
            _context.EventGameBus.Publish(new GameStateChanged() { NewState = state});
        }

        private async UniTaskVoid RestartAsync()
        {
            var restartDelay = _configData.RestartWaitTime;
            await UniTask.WaitForSeconds((int)restartDelay / 2f);
            _model.ChangeGameState(GameState.RestartDelay);
            await UniTask.WaitForSeconds((int)restartDelay / 2f);
            _model.ChangeGameState(GameState.PressAny);
        }
    }
}