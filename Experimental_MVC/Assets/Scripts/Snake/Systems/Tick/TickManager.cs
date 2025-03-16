using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using SnakeExample.Config;
using SnakeExample.Events;
using Zenject;

namespace SnakeExample.Tick
{
    internal class TickManager : IController, ISceneLifeCycleManaged
    {
        [Inject] private GameConfigDataSO _configData;
        [Inject] private IEventBus<GameEvent> _eventBus;
        private bool _isRunning = false;
        public void OnAwakeCallback()
        {
            _isRunning = true;
            TickAsync().Forget();
        }

        public void OnDestroyCallback()
        {
            _isRunning = false;
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
    }
}
