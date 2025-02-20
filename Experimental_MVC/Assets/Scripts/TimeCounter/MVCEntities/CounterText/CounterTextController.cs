using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using TimeCounter.Commands;
using TimeCounter.Events.CoreEvents;

namespace TimeCounter.Entities.CounterText
{
    public class CounterTextController : BaseController<ICounterTextModel, ICounterTextView, ICounterTextContext>, ILifeCycleHandler
    {
        public CounterTextController(ICounterTextModel model, ICounterTextView view, ICounterTextContext context) : base(model, view, context)
        {
        }
        private DisposableBag _dataBindingDisposableBag;
        public void Initialize()
        {
            _model.Setup(_context);
            _view.Setup(_context);
            _model.CounterText.Subscribe(_view.OnCounterTextUpdated).AddTo(ref _dataBindingDisposableBag);
            _model.AnimatorSpeed.Subscribe(_view.OnAnimatorPlaybackSpeedChanged).AddTo(ref _dataBindingDisposableBag);
            SubscribeEvents();
        }

        public void Dispose()
        {
            _dataBindingDisposableBag.Dispose();
            _model.Dispose();
            _view.Dispose();
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _context.EventBusCore.Subscribe<TickCountValueUpdatedEvent>(OnTickValueUpdated);
        }
        private void UnsubscribeEvents()
        {
            _context.EventBusCore.Unsubscribe<TickCountValueUpdatedEvent>(OnTickValueUpdated);
        }

        private void OnTickValueUpdated(TickCountValueUpdatedEvent @event)
        {
            _model.UpdateTextWithTickValue(@event.UpdatedValue);
            if (@event.UpdatedValue > 0)
                _context.CommandManager.ExecuteCommand(new AnimateCounterTextCommand(_model.TriggerHash, UnityEngine.AnimatorControllerParameterType.Trigger));
        }
    }
}
