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
    public class TickerController : BaseControllerWithModelAndContext<ITickerModel, ITickerContext>, ISceneLifeCycleManaged
    {
        private CancellationTokenSource _tickCancellationTokenSource;
        private IDisposable _modelSubDisposable;
        private TickCountType _tickCountingType;
        public TickerController(ITickerModel model, ITickerContext context) : base(model, context)
        {
           
        }
        public void OnAwakeCallback()
        {
            _model.Setup(_context);
            _context.EventBusGlobal.Subscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _context.EventBusCore.Subscribe<StartStopCounterEvent>(HandleOnStartStopCounter);

            var modelDisposableBuilder = Disposable.CreateBuilder();
            _model.TickCount.Subscribe(HandleOnTickCountValueUpdated).AddTo(ref modelDisposableBuilder);
            _model.TickSpeed.Subscribe(HandleOnTickSpeedValueUpdated).AddTo(ref modelDisposableBuilder);
            _modelSubDisposable = modelDisposableBuilder.Build();
        }

        private void HandleOnStartStopCounter(StartStopCounterEvent @event)
        {
            _model.IsTickEnabled = !_model.IsTickEnabled;
            if (_model.IsTickEnabled)
            {
                StartTick();
            }
            else
            {
                StopTick();
            }
        }

        private void HandleOnTickSpeedValueUpdated(float value)
        {
            _context.EventBusCore.Publish(new TickSpeedUpdatedEvent() { UpdatedValue = _model.TickSpeed.CurrentValue });
        }

        public override void Dispose()
        {
            _context.EventBusGlobal.Unsubscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _context.EventBusCore.Unsubscribe<StartStopCounterEvent>(HandleOnStartStopCounter);
            _modelSubDisposable.Dispose();
            StopTick();
            base.Dispose();
        }
        public void OnDestroyCallback()
        {
            Dispose();
        }
        private void HandleOnTickCountValueUpdated(int newValue)
        {
            _context.EventBusCore.Publish(new TickCountValueUpdatedEvent() { UpdatedValue = newValue });
        }

        private void HandleOnSceneInitialized(SceneInitializedEvent @event)
        {
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
        private void StartTick()
        {
            _tickCancellationTokenSource = new CancellationTokenSource();
            ActivateTick().Forget();
        }
        private async UniTask ActivateTick()
        {
            while (true)
            { 
                var countSpeed = UnityEngine.Mathf.Max(_model.TickSpeed.CurrentValue, 0.01f);
                var secondsToWait = (float)(1f / countSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000), cancellationToken: _tickCancellationTokenSource.Token);

                if (_tickCancellationTokenSource.Token.IsCancellationRequested)
                    break;

                HandleOnTick();
            }
        }
        private void StopTick()
        {
            _tickCancellationTokenSource?.Cancel();
            _tickCancellationTokenSource?.Dispose();
            _tickCancellationTokenSource = null;
        }
    }
}
