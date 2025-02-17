using Assets.Scripts.Batuhan.RuntimeCopyScriptableObjects;
using Batuhan.MVC.Base;
using System;
using TimeCounter.Data;
using TimeCounter.Events.ModelEvents;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextModel : BaseModel<ICounterTextContext>
    {
        private CounterTextModelDataSO _data;

        public float CountSpeed => _data.CountSpeed;
        public CounterTextModel(CounterTextModelDataSO initialData, RuntimeClonableSOManager clonableSOManager)
        {
            _data = clonableSOManager.CreateModelDataSOInstance(initialData);
        }

        public override void Setup(ICounterTextContext context)
        {
            base.Setup(context);
            _data.CounterValue = 0;
            _data.CountSpeed = 10f;
            _context.Debug.Log("Setup", this);
        }

        public override void Dispose()
        {
        }
        public void IncreaseCounter(int value = 1)
        {
            var oldValue = _data.CounterValue;
            var newValue = Math.Max(_data.CounterValue + value, 0);
            _data.CounterValue = newValue;

            if (oldValue != newValue)
            {
                _context.EventBusModel.Publish(new CounterValueUpdatedEvent() { UpdatedValue = _data.CounterValue });
            }
            else
            {
                _context.Debug.Log("Unable to update counter value", this);
            }
        }
    }
}
