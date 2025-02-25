using Batuhan.EventBus;
using Batuhan.MVC.Core;

namespace SnakeExample.Entities.GameManager
{
    public interface IGameManagerContext : IContext
    {
    }
    public class GameManagerContext : IContext
    {
        public void Dispose()
        {
        }
    }
}
