using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using System;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextModel : BaseModel
    {
        private ICounterTextContext _context;
        private float _countSpeed = 1.0f;
        private int _counterValue = 0;

        public float CountSpeed
        {
            get
            {
                if (_countSpeed <= 0)
                {
                    throw new Exception("Count Speed cannot be less than or equal to 0");
                }

                return _countSpeed;
            }
        }
        public float CounterValue { get => _counterValue; }
        public override IContext Context { get => _context; }

        public void Initialize()
        {
            _counterValue = 0;
            _countSpeed = 1f;
        }

        //TODO Implement Observable Pattern
        public void IncreaseCounter(int value = 1)
        {
            _counterValue += value;
            _context.            
        }
    }
}
