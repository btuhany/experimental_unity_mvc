using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using TimeCounter.Commands;
using TimeCounter.Events.CoreEvents;
namespace TimeCounter.Entities.CounterText
{
    public class CounterTextController : BaseController<ICounterTextModel, IViewContextual<ICounterTextContext>, ICounterTextContext>, ILifeCycleHandler
    {
        public CounterTextController(ICounterTextModel model, IViewContextual<ICounterTextContext> view, ICounterTextContext context) : base(model, view, context)
        {

        }

        public void Initialize()
        {
            _model.Setup(_context);
            _view.Setup(_context);
            SubscribeEvents();
        }

        public void Dispose()
        {
            _model.Dispose();
            _view.Dispose();
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _context.EventBusCore.Subscribe<TimeCountValueUpdatedEvent>(OnCountValueUpdated);
           
        }
        private void UnsubscribeEvents()
        {
            
            _context.EventBusCore.Unsubscribe<TimeCountValueUpdatedEvent>(OnCountValueUpdated);
        }

        private void OnCountValueUpdated(TimeCountValueUpdatedEvent @event)
        {
            _model.UpdateTextWithValue(@event.UpdatedValue);
            _context.CommandManager.ExecuteCommand(new UpdateCounterTextCommand(_model.TEXT));
        }
    }
}
