using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TimeCounter.Commands;
using TimeCounter.Events.CoreEvents;
using TimeCounter.Events.GlobalEvents;
using TimeCounter.Events.ModelEvents;

namespace TimeCounter.Entities.CounterText
{
    [Serializable]
    internal class CounterTextController : BaseController<CounterTextModel, CounterTextView, ICounterTextContext>, ILifeCycleHandler
    {
        private CancellationToken _tickCancellationToken;

        public CounterTextController(CounterTextModel model, CounterTextView view, ICounterTextContext context) : base(model, view, context)
        {
            _tickCancellationToken = new CancellationToken();
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
                _tickCancellationToken.ThrowIfCancellationRequested();

                var secondsToWait = (float)(1f / _model.CountSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000), cancellationToken: _tickCancellationToken);
                _model.IncreaseCounter();
                _context.Debug.Log("tick!", this);
            }
        }
    }
}
