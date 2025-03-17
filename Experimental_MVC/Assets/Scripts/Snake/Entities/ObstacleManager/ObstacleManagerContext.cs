using Batuhan.MVC.Core;
using SnakeExample.Grid;
using Zenject;

namespace SnakeExample.Entities.ObstacleManager
{
    internal class ObstacleManagerContext : IContext
    {
        [Inject] public IGridViewHelper GridView { get; }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
