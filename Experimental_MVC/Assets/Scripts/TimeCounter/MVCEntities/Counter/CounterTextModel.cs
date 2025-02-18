using Assets.Scripts.Batuhan.RuntimeCopyScriptableObjects;
using Batuhan.MVC.Core;
using System;
using TimeCounter.Data;
using TimeCounter.Events.ModelEvents;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextModel :  IModelContextual<ICounterTextContext>
    {
        void IncreaseCounter(int value);
        float CountSpeed { get; }
    }
    internal class CounterTextModel : ICounterTextModel
    {
        private ICounterTextContext _context;
        private CounterTextModelDataSO _dataSO;
        public float CountSpeed => _dataSO.CountSpeed;
        public ICounterTextContext Context => _context;

        public CounterTextModel(CounterTextModelDataSO initialData, RuntimeClonableSOManager clonableSOManager)
        {
            _dataSO = clonableSOManager.CreateModelDataSOInstance(initialData);
        }

        public void Setup(ICounterTextContext context)
        {
            _context = context;
            _context.Debug.Log("Setup", this);
        }

        public void Dispose()
        {
        }
        public void IncreaseCounter(int value = 1)
        {
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
