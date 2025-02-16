using Batuhan.MVC.Base;
using System;
using TimeCounter.Events.ModelEvents;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextModel : BaseModel<ICounterTextContext>
    {
        private float _countSpeed = 1.0f;
        private int _counterValue = 0;

        public float CountSpeed { get => _countSpeed; }
        public int CounterValue { get => _counterValue; }
        public override void Setup(ICounterTextContext context)
        {
            base.Setup(context);
            _counterValue = 0;
            _countSpeed = 1f;
            _context.Debug.Log("Setup", this);
        }

        public override void Dispose()
        {

        }
        public void IncreaseCounter(int value = 1)
        {
            var oldValue = _counterValue;
            var newValue = Math.Max(_counterValue + value, 0);
            _counterValue = newValue;

            if (oldValue != newValue)
            {
                _context.EventBusModel.Publish(new CounterValueUpdatedEvent() { UpdatedValue = _counterValue });
            }
            else
            {
                _context.Debug.Log("Unable to update counter value", this);
            }
        }


    }
}
