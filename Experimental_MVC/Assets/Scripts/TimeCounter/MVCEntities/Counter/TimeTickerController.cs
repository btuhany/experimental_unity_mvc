using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using TimeCounter.Events.CoreEvents;
using TimeCounter.Events.GlobalEvents;

namespace TimeCounter.Entities.Counter
{
    public class TimeTickerController : BaseControllerWithModelAndContext<ITimeTickerModel, ITimeTickerContext>, ILifeCycleHandler
    {
        private CancellationTokenSource _tickCancellationTokenSource;
        private IDisposable _modelSubDisposable;
        public TimeTickerController(ITimeTickerModel model, ITimeTickerContext context) : base(model, context)
        {
            _tickCancellationTokenSource = new CancellationTokenSource();
        }
        public void Initialize()
        {
            _model.Setup(_context);
            _context.EventBusGlobal.Subscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _modelSubDisposable = _model.TickCount.Subscribe(HandleOnTickCountValueUpdated);
        }
        public void Dispose()
        {
            _context.EventBusGlobal.Unsubscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _modelSubDisposable.Dispose();
            DeactivateTick();
        }

        private void HandleOnTickCountValueUpdated(int newValue)
        {
            _context.EventBusCore.Publish(new TickCountValueUpdatedEvent() { UpdatedValue = newValue });
        }

        private void HandleOnSceneInitialized(SceneInitializedEvent @event)
        {
            _context.Debug.Log("Handle on scene init", this);
            ActivateTick().Forget();
        }
        private void HandleOnTick()
        {
            _model.IncreaseCounter(1);
            
            _context.Debug.Log("tick!", this);
        }
        private async UniTask ActivateTick()
        {
            while (true)
            {
                var countSpeed = UnityEngine.Mathf.Max(_model.TickSpeed, 0.01f);
                var secondsToWait = (float)(1f / countSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000), cancellationToken: _tickCancellationTokenSource.Token);

                if (_tickCancellationTokenSource.Token.IsCancellationRequested)
                    break;

                HandleOnTick();
            }
        }
        private void DeactivateTick()
        {
            _tickCancellationTokenSource?.Cancel();
            _tickCancellationTokenSource?.Dispose();
            _tickCancellationTokenSource = null;
        }
    }
}
