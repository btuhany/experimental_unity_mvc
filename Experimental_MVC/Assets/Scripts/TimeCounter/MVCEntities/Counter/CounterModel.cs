using Batuhan.MVC.Core;
using Batuhan.RuntimeCopyScriptableObjects;
using System;
using TimeCounter.Data;
using TimeCounter.Entitites.Counter;
using TimeCounter.Events.ModelEvents;

namespace TimeCounter.Entities.Counter
{
    public interface ICounterModel : IModelContextual<ICounterContext>
    {
        void CreateData(CounterModelDataSO initialData, RuntimeClonableSOManager clonableSOManager);
        void IncreaseCounter(int value);
        float CountSpeed { get; }
        Action<int> OnCountValueChanged { get; set; } //TODOby reactive property unirx
    }
    public class CounterModel : ICounterModel
    {
        private ICounterContext _context;
        private CounterModelDataSO _dataSO;
        public float CountSpeed => _dataSO.CountSpeed;
        public ICounterContext Context => _context;

        public Action<int> OnCountValueChanged { get; set; }

        [Zenject.Inject]
        public void CreateData(CounterModelDataSO initialData, RuntimeClonableSOManager clonableSOManager)
        {
            _dataSO = clonableSOManager.CreateModelDataSOInstance(initialData);
        }

        public void Setup(ICounterContext context)
        {
            _context = context;
            _context.Debug.Log("Setup", this);
        }

        public void Dispose()
        {
        }
        public void IncreaseCounter(int value = 1)
        {
            if (value < 0)
            {
                _context.Debug.Log("Unable to update counter value", this);
                return;
            }

            var oldValue = _dataSO.CounterValue;
            var newValue = Math.Max(_dataSO.CounterValue + value, 0);
            _dataSO.CounterValue = newValue;
            if (oldValue != newValue)
            {
                _context.EventBusModel.Publish(new CounterValueUpdatedEvent() { UpdatedValue = _dataSO.CounterValue });
            }
            else
            {
                _context.Debug.Log("Unable to update counter value", this);
            }
        }
    }
}
