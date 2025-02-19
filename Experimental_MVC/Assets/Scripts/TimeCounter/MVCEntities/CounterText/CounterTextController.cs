using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using TimeCounter.Commands;
using TimeCounter.Events.CoreEvents;
using TimeCounter.Events.ModelEvents;
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
            _context.Debug.Log("Disposed!", this);
            _model.Dispose();
            _view.Dispose();


            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _context.EventBusModel.Subscribe<CounterValueUpdatedEvent>(OnCountValueUpdated);
           
        }
        private void UnsubscribeEvents()
        {
            
            _context.EventBusModel.Unsubscribe<CounterValueUpdatedEvent>(OnCountValueUpdated);
        }


        private void OnCountValueUpdated(CounterValueUpdatedEvent @event)
        {
            _context.Debug.Log("OnCountValueUpdated");
            _context.CommandManager.ExecuteCommand(new UpdateCounterTextCommand(@event.UpdatedValue));
            _context.EventBusCore.Publish(new TimeCountValueUpdatedEvent(@event.UpdatedValue));
        }
    }
}
