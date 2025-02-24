using Batuhan.MVC.Core;

namespace SnakeExample.Entities.InputReader
{
    public interface IInputReaderContext : IContext
    {

    }
    public class InputReaderContext : IInputReaderContext
    {
        public void Dispose()
        {
        }
    }
}
