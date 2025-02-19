using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using System;
using TimeCounter.Commands;
using TimeCounter.Events.CoreEvents;

namespace TimeCounter.Entities.CounterText
{
    public class CounterTextController : BaseController<ICounterTextModel, ICounterTextView, ICounterTextContext>, ILifeCycleHandler
    {
        public CounterTextController(ICounterTextModel model, ICounterTextView view, ICounterTextContext context) : base(model, view, context)
        {
        }
        private IDisposable _dataBindingDisposable;
        //private TriggerAnimatorHashCommand _animateCounterTextCommand;
        public void Initialize()
        {
            _model.Setup(_context);
            _view.Setup(_context);
            _dataBindingDisposable = _model.CounterText.Subscribe(_view.OnCounterTextUpdated);
            //_animateCounterTextCommand = new TriggerAnimatorHashCommand(_model.TriggerHash, _view.Animator);
            SubscribeEvents();
        }

        public void Dispose()
        {
            _dataBindingDisposable?.Dispose();
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
            //_context.CommandManager.ExecuteCommand(_animateCounterTextCommand); //Or we can make view listen to command like a event and handle the animation
            if (@event.UpdatedValue > 0)
                _context.CommandManager.ExecuteCommand(new AnimateCounterTextCommand(_model.TriggerHash, UnityEngine.AnimatorControllerParameterType.Trigger));
        }
    }
}
