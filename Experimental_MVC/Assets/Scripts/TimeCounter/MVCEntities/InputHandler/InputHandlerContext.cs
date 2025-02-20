using Batuhan.MVC.Core;
using TimeCounter.Entities.Counter;
using Zenject;

namespace TimeCounter.Entities.InputHandler
{
    public interface IInputHandlerContext : IContext
    {
        ITickerModel TickerModel { get; }
    }
    public class InputHandlerContext : IInputHandlerContext
    {
        [Inject]
        public ITickerModel TickerModel { get; }

        public void Dispose()
        {
        }
    }
}
    
