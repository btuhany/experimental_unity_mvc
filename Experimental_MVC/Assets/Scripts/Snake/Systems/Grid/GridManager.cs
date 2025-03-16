using Batuhan.GridSystem.WorldGrid;
using Batuhan.MVC.Core;
using UnityEngine;

namespace SnakeExample.Grid
{
    internal interface IGridViewHelper
    {
        Vector3 GetWorldPositionCenter(int x, int y);
    }

    internal class GridSystem : WorldGrid2D<IGridObject>
    {
        public GridSystem(int width, int height, float cellSize, Vector3 origin, IWorldCoordinateConverter worldConverter) : base(width, height, cellSize, origin, worldConverter)
        {
        }
    }
    internal class GridManager : IController, ISceneLifeCycleManaged, IGridViewHelper
    {
        private GridSystem _grid;

        public GridManager()
        {
            _grid = new GridSystem(10, 10, 1f, Vector3.zero, new VerticalConverter());
        }
        public void OnAwakeCallback()
        {
            
        }

        public void OnDestroyCallback()
        {
        }
        public Vector3 GetWorldPositionCenter(int x, int y) => _grid.GetWorldPositionCenter(x, y);
    }
}
