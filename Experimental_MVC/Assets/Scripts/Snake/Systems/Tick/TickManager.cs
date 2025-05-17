using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using SnakeExample.Config;
using SnakeExample.Events;
using System;
using Zenject;

namespace SnakeExample.Tick
{
    internal class TickManager : IController, ISceneLifeCycleManaged, IEntryPoint
    {
        [Inject] private GameConfigDataSO _configData;
        [Inject] private IEventBus<GameEvent> _eventBus;
        private bool _isRunning = false;
        public void OnAwakeCallback()
        {
            _eventBus.Subscribe<GameStateChanged>(OnGameStateChanged);
        }

        public void OnDestroyCallback()
        {
            _eventBus.Unsubscribe<GameStateChanged>(OnGameStateChanged);
        }
        private void OnGameStateChanged(GameStateChanged changed)
        {
            if (changed.NewState == Entities.GameManager.GameState.Started)
            {
                _isRunning = true;
                TickAsync().Forget();
            }
            else
            {
                _isRunning = false;
            }
        }

        private async UniTaskVoid TickAsync()
        {
            while (_isRunning)
            {
                _eventBus.Publish(new TickEvent());
                //UnityEngine.Debug.Log("Tick");
                await UniTask.Delay(_configData.TickIntervalsMillisecond);
            }
        }

        public void OnStartCallback()
        {
            _eventBus.Publish(new SceneInitializationEvent());
        }
    }
}
