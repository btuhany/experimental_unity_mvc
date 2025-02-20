using Batuhan.MVC.Core;
using Batuhan.RuntimeCopyScriptableObjects;
using Cysharp.Threading.Tasks;
using R3;
using System;
using TimeCounter.Data;

namespace TimeCounter.Entities.Counter
{
    public interface ITickerModel : IModelContextual<ITickerContext>
    {
        void CreateData(TimeTickerModelDataSO initialData, RuntimeClonableSOManager clonableSOManager);
        void IncreaseCounter(int value);
        void DecreaseCounter(int value);
        void AddToTickSpeed(float value);
        bool IsMaxTickCountReached();
        bool IsMinTickCountReached();
        void SetMaxTickCount(int value);
        void SetMinTickCount(int value);
        bool TrySetTickCount(int value);
        ReadOnlyReactiveProperty<int> TickCount { get; }
        ReadOnlyReactiveProperty<float> TickSpeed { get; }
    }
    public class TickerModel : ITickerModel
    {
        private ITickerContext _context;
        private TimeTickerModelDataSO _dataSO;

        public ITickerContext Context => _context;

        public ReadOnlyReactiveProperty<int> TickCount => _dataSO.TickCount;

        public ReadOnlyReactiveProperty<float> TickSpeed => _dataSO.TickSpeed;

        [Zenject.Inject]
        public void CreateData(TimeTickerModelDataSO initialData, RuntimeClonableSOManager clonableSOManager)
        {
            _dataSO = clonableSOManager.CreateModelDataSOInstance(initialData);
        }

        public void Setup(ITickerContext context)
        {
            _context = context;
            _dataSO.Initialize();
            _context.Debug.Log("Setup", this);
        }

        public void Dispose()
        {
            UnityEngine.Debug.Log("dISPOSESESESESE");
            _dataSO.Dispose();
        }
        public void IncreaseCounter(int value = 1)
        {
            if (value < 0)
            {
                _context.Debug.Log("Unable to update counter value", this);
                return;
            }

            TrySetTickCount(_dataSO.TickCount.Value + value);
        }
        public bool IsMaxTickCountReached()
        {
            return _dataSO.MaxTickCountReached;
        }
        public bool IsMinTickCountReached()
        {
            return _dataSO.MinTickCountReached;
        }
        public void DecreaseCounter(int value)
        {
            if (value < 0)
            {
                _context.Debug.Log("Unable to update counter value", this);
                return;
            }

            TrySetTickCount(_dataSO.TickCount.Value - value);
        }
        public void AddToTickSpeed(float value)
        {
            _dataSO.TickSpeed.Value = UnityEngine.Mathf.Clamp(_dataSO.TickSpeed.Value + value, _dataSO.MinTickSpeed, _dataSO.MaxTickSpeed);
        }

        public void SetMaxTickCount(int value)
        {
            _dataSO.MaxTickCount = value;
        }

        public void SetMinTickCount(int value)
        {
            _dataSO.MinTickCount = value;
        }

        public bool TrySetTickCount(int value)
        {
            var oldValue = _dataSO.TickCount.Value;
            var newValue = UnityEngine.Mathf.Clamp(value, _dataSO.MinTickCount, _dataSO.MaxTickCount);
            if (oldValue != newValue)
            {
                _dataSO.TickCount.Value = newValue;
                return true;
            }
            return false;
        }
    }
}
