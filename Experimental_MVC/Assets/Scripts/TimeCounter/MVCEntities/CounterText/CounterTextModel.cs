using Batuhan.MVC.Core;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextModel :  IModelContextual<ICounterTextContext>
    {
        public void UpdateTextWithValue(int value);
        public string TEXT { get; }
    }
    public class CounterTextModel : ICounterTextModel
    {
        private string _textStr;
        private const string _prefix = "EXPERIMENTAL_MVC: ";
        private ICounterTextContext _context;
        public ICounterTextContext Context => _context;

        //TODOBY
        public string TEXT => _textStr;

        public void Setup(ICounterTextContext context)
        {
            _context = context;
            _context.Debug.Log("Setup", this);
        }

        public void Dispose()
        {
        }

        public void UpdateTextWithValue(int value)
        {
            _textStr = _prefix + value.ToString();
        }
    }
}
