using Batuhan.MVC.Core;
using R3;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextModel :  IModelContextual<ICounterTextContext>
    {
        public void UpdateTextWithTickValue(int value);
        ReactiveProperty<string> CounterText { get; }
    }
    public class CounterTextModel : ICounterTextModel
    {
        private const string _prefix = "EXPERIMENTAL MVC | TICK COUNT: ";
        private ICounterTextContext _context;
        public ICounterTextContext Context => _context;

        public ReactiveProperty<string> CounterText { get; private set; }

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
            CounterText.Value = _prefix + value.ToString();
        }
    }
}
