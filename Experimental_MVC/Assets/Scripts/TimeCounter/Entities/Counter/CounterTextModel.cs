using Assets.Scripts.Batuhan.RuntimeCopyScriptableObjects;
using Batuhan.MVC.Base;
using System;
using TimeCounter.Data;
using TimeCounter.Events.ModelEvents;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextModel : BaseModel<ICounterTextContext>
    {
        private CounterTextModelDataSO _dataSO;
        private RuntimeClonableSOManager _runtimeClonableSOManager;
        public float CountSpeed => _dataSO.CountSpeed;
        public CounterTextModel(CounterTextModelDataSO initialData, RuntimeClonableSOManager clonableSOManager)
        {
            _dataSO = initialData;
            _runtimeClonableSOManager = clonableSOManager;
        }

        public override void Setup(ICounterTextContext context)
        {
            base.Setup(context);
            _dataSO = _runtimeClonableSOManager.CreateModelDataSOInstance(_dataSO);
            _dataSO.CounterValue = 0;
            _dataSO.CountSpeed = 10f;
            _context.Debug.Log("Setup", this);
        }

        public override void Dispose()
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
