using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using System.Threading;
using TimeCounter.Commands;
using TimeCounter.Events.CoreEvents;
using TimeCounter.Events.GlobalEvents;
using TimeCounter.Events.ModelEvents;
using UnityEngine;

namespace TimeCounter.Entities.CounterText
{
    public class CounterTextController : BaseController<ICounterTextModel, IViewContextual<ICounterTextContext>, ICounterTextContext>, ILifeCycleHandler
    {
        private CancellationTokenSource _tickCancellationTokenSource;

        public CounterTextController(ICounterTextModel model, IViewContextual<ICounterTextContext> view, ICounterTextContext context) : base(model, view, context)
        {
            _tickCancellationTokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _context.Debug.Log("Initialized!", this);
            _model.Setup(_context);
            _view.Setup(_context);

            SubscribeEvents();
        }

        public void Dispose()
        {
            _context.Debug.Log("Disposed!", this);
            _model.Dispose();
            _view.Dispose();

            DeactivateTick();
            UnsubscribeEvents();
        }
        private void SubscribeEvents()
        {
            _context.EventBusModel.Subscribe<CounterValueUpdatedEvent>(OnCountValueUpdated);
            _context.EventBusGlobal.Subscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
        }
        private void UnsubscribeEvents()
        {
            _context.EventBusGlobal.Unsubscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _context.EventBusModel.Unsubscribe<CounterValueUpdatedEvent>(OnCountValueUpdated);
        }
        private void HandleOnSceneInitialized(SceneInitializedEvent @event)
        {
            _context.Debug.Log("Handle on scene init", this);
            ActivateTick().Forget();
        }

        private void OnCountValueUpdated(CounterValueUpdatedEvent @event)
        {
            _context.Debug.Log("OnCountValueUpdated");
            _context.CommandManager.ExecuteCommand(new UpdateCounterTextCommand(@event.UpdatedValue));
            _context.EventBusCore.Publish(new TimeCountValueUpdatedEvent(@event.UpdatedValue));
        }
        private async UniTask ActivateTick()
        {
            while (true)
            {
                var countSpeed = Mathf.Max(_model.CountSpeed, 0.01f);
                var secondsToWait = (float) (1f / countSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000), cancellationToken: _tickCancellationTokenSource.Token);

                if (_tickCancellationTokenSource.Token.IsCancellationRequested)
                    break;

                _model.IncreaseCounter(1);
                _context.Debug.Log("tick!", this);
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
