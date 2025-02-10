using Assets.Scripts.Batuhan.Core.MVC.Base;
using Batuhan.Core.MVC;
using System;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterModel : BaseModel
    {
        private float _countSpeed = 1.0f;
        private int _counterValue = 0;

        public Action<int> OnCountValueChanged;  //TODO EventManager / Observer Pattern

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
        public void Initialize()
        {
            _counterValue = 0;
            _countSpeed = 1f;
        }

        //TODO Implement Observable Pattern
        public void IncreaseCounter(int value = 1)
        {
            _counterValue += value;
            OnCountValueChanged?.Invoke(_counterValue);
        }
    }
}
