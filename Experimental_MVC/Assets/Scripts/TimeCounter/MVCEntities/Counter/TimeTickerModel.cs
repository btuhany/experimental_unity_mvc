using Batuhan.MVC.Core;
using Batuhan.RuntimeCopyScriptableObjects;
using System;
using TimeCounter.Data;

namespace TimeCounter.Entities.Counter
{
    public interface ITimeTickerModel : IModelContextual<ITimeTickerContext>
    {
        void CreateData(TimeTickerModelDataSO initialData, RuntimeClonableSOManager clonableSOManager);
        void IncreaseCounter(int value);
        float CountSpeed { get; }
        Action<int> OnCountValueChanged { get; set; } //TODOby reactive property unirx
    }
    public class TimeTickerModel : ITimeTickerModel
    {
        private ITimeTickerContext _context;
        private TimeTickerModelDataSO _dataSO;
        public float CountSpeed => _dataSO.CountSpeed;
        public ITimeTickerContext Context => _context;

        public Action<int> OnCountValueChanged { get; set; }

        [Zenject.Inject]
        public void CreateData(TimeTickerModelDataSO initialData, RuntimeClonableSOManager clonableSOManager)
        {
            _dataSO = clonableSOManager.CreateModelDataSOInstance(initialData);
        }

        public void Setup(ITimeTickerContext context)
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
                OnCountValueChanged?.Invoke(newValue);
            }
            else
            {
                _context.Debug.Log("Unable to update counter value", this);
            }
        }
    }
}
