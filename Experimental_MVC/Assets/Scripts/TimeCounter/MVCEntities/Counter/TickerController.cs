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
    public enum TickCountType
    {
        Increasing = 0,
        Decreasing = 1
    }
    public class TickerController : BaseControllerWithModelAndContext<ITickerModel, ITickerContext>, ILifeCycleHandler
    {
        private CancellationTokenSource _tickCancellationTokenSource;
        private IDisposable _modelSubDisposable;
        private TickCountType _tickCountingType;
        public TickerController(ITickerModel model, ITickerContext context) : base(model, context)
        {
            _tickCancellationTokenSource = new CancellationTokenSource();
        }
        public void Initialize()
        {
            _model.Setup(_context);
            _context.EventBusGlobal.Subscribe<SceneInitializedEvent>(HandleOnSceneInitialized);

            var modelDisposableBuilder = Disposable.CreateBuilder();
            _model.TickCount.Subscribe(HandleOnTickCountValueUpdated).AddTo(ref modelDisposableBuilder);
            _model.TickSpeed.Subscribe(HandleOnTickSpeedValueUpdated).AddTo(ref modelDisposableBuilder);
            _modelSubDisposable = modelDisposableBuilder.Build();
        }

        private void HandleOnTickSpeedValueUpdated(float value)
        {
            //TODOBY
        }

        public void Dispose()
        {
            _context.EventBusGlobal.Unsubscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _modelSubDisposable.Dispose();
            _model.Dispose();
            DeactivateTick();
        }
        private void IncreaseTickSpeed(int value)
        {
           _model.IncreaseTickSpeed(value);
        }
        private void HandleOnTickCountValueUpdated(int newValue)
        {
            _context.EventBusCore.Publish(new TickCountValueUpdatedEvent() { UpdatedValue = newValue });
        }

        private void HandleOnSceneInitialized(SceneInitializedEvent @event)
        {
            ActivateTick().Forget();
        }
        private void HandleOnTick()
        {
            CheckHasCountingTypeChanged();
            HandleCounter();
        }

        private void CheckHasCountingTypeChanged()
        {
            if (_model.IsMaxTickCountReached())
                _tickCountingType = TickCountType.Decreasing;
            if (_model.IsMinTickCountReached())
                _tickCountingType = TickCountType.Increasing;
        }
        private void HandleCounter()
        {
            if (_tickCountingType == TickCountType.Increasing)
            {
                _model.IncreaseCounter(1);
            }
            else if (_tickCountingType == TickCountType.Decreasing)
            {
                _model.DecreaseCounter(1);
            }
        }
        private async UniTask ActivateTick()
        {
            while (true)
            {
                var countSpeed = UnityEngine.Mathf.Max(_model.TickSpeed.Value, 0.01f);
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
