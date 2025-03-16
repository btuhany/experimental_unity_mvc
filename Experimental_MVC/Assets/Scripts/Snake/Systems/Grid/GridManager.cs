using Batuhan.GridSystem.WorldGrid;
using Batuhan.MVC.Core;
using SnakeExample.Config;
using UnityEngine;

namespace SnakeExample.Grid
{
    internal interface IGridViewHelper
    {
        Vector3 GetWorldPositionCenter(int x, int y);
    }
    internal interface IGridModelHelper
    {
        GridSystem Grid { get; }
    }

    internal class GridSystem : WorldGrid2D<IGridObject>
    {
        public GridSystem(int width, int height, float cellSize, Vector3 origin, IWorldCoordinateConverter worldConverter) : base(width, height, cellSize, origin, worldConverter)
        {
        }
        public override bool TrySetElement(int x, int y, IGridObject element)
        {
            if (!IsOccupied(x, y))
            {
                return base.TrySetElement(x, y, element);
            }
            return false;
        }
    }
    internal class GridManager : IController, ISceneLifeCycleManaged, IGridViewHelper, IGridModelHelper
    {
        private GridSystem _grid;

        public GridSystem Grid => _grid;

        public GridManager(GameConfigDataSO configData)
        {
            _grid = new GridSystem(configData.GridWidth, configData.GridHeight, configData.GridCellSize, configData.GridOriginPos, new VerticalConverter());
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
