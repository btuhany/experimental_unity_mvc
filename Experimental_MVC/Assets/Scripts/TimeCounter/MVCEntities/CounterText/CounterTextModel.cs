using Batuhan.MVC.Core;
using R3;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextModel :  IModelContextual<ICounterTextContext>
    {
        public void UpdateTextWithTickValue(int value);
        ReactiveProperty<string> CounterText { get; }
        public int TriggerHash { get; }
    }
    public class CounterTextModel : ICounterTextModel
    {

        private ICounterTextContext _context;
        public ICounterTextContext Context => _context;

        public ReactiveProperty<string> CounterText { get; private set; }

        private readonly int _triggerHash = UnityEngine.Animator.StringToHash("trigger");

        public int TriggerHash => _triggerHash;

        public void Setup(ICounterTextContext context)
        {
            _context = context;
            _context.Debug.Log("Setup", this);
            CounterText = new(string.Empty);
        }

        public void Dispose()
        {
            CounterText?.Dispose();
        }

        public void UpdateTextWithTickValue(int value)
        {
            CounterText.Value = value.ToString();
        }
    }
}
