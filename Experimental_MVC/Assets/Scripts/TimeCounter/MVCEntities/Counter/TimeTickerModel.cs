using Batuhan.MVC.Core;
using Batuhan.RuntimeCopyScriptableObjects;
using Cysharp.Threading.Tasks;
using R3;
using System;
using TimeCounter.Data;

namespace TimeCounter.Entities.Counter
{
    public interface ITimeTickerModel : IModelContextual<ITimeTickerContext>
    {
        void CreateData(TimeTickerModelDataSO initialData, RuntimeClonableSOManager clonableSOManager);
        void IncreaseCounter(int value);
        float TickSpeed { get; }
        ReactiveProperty<int> TickCount { get; }
    }
    public class TimeTickerModel : ITimeTickerModel
    {
        private ITimeTickerContext _context;
        private TimeTickerModelDataSO _dataSO;
        private IDisposable _disposable;
        public float TickSpeed => _dataSO.TickSpeed;
        public ITimeTickerContext Context => _context;

        public ReactiveProperty<int> TickCount { get; private set; }

        [Zenject.Inject]
        public void CreateData(TimeTickerModelDataSO initialData, RuntimeClonableSOManager clonableSOManager)
        {
            var disposable = Disposable.CreateBuilder();
            _dataSO = clonableSOManager.CreateModelDataSOInstance(initialData);

            TickCount = new ReactiveProperty<int>(_dataSO.TickCount).AddTo(ref disposable);
            TickCount.Subscribe(newValue => _dataSO.TickCount = newValue).AddTo(ref disposable);

            _disposable = disposable.Build();
        }

        public void Setup(ITimeTickerContext context)
        {
            _context = context;
            _context.Debug.Log("Setup", this);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
        public void IncreaseCounter(int value = 1)
        {
            if (value < 0)
            {
                _context.Debug.Log("Unable to update counter value", this);
                return;
            }

            var oldValue = TickCount.Value;
            var newValue = Math.Max(oldValue + value, 0);

            if (oldValue != newValue)
            {
                TickCount.Value = newValue;
            }
            else
            {
                _context.Debug.Log("Unable to update counter value", this);
            }
        }
    }
}
