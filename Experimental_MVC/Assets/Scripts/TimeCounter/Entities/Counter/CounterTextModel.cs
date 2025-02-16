using Assets.Scripts.TimeCounter.Helper;
using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using System;
using TimeCounter.Events.ModelEvents;
using UnityEngine;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextModel : BaseModel
    {
        private ICounterTextContext _context;
        private float _countSpeed = 1.0f;
        private int _counterValue = 0;

        public float CountSpeed { get => _countSpeed; }
        public int CounterValue { get => _counterValue; }
        public override IContext Context { get => _context; }
        public void Setup(ICounterTextContext context)
        {
            _context = context;
            _counterValue = 0;
            _countSpeed = 1f;
            _context.Debug.Log("Setup", this);
        }

        public void Dispose()
        {
            _context.Debug.Log("Disposed", this);
        }
        public void IncreaseCounter(int value = 1)
        {
            var oldValue = _counterValue;
            var newValue = Math.Max(_counterValue + value, 0);

            if (oldValue != newValue)
            {
                _context.EventBusModel.Publish(new CountValueUpdatedEvent() { NewValue = value });
            }
            else
            {
                _context.Debug.Log("Unable to update counter value", this);
            }
        }


    }
}
