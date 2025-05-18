using Batuhan.MVC.Core;
using BatuhanYigit.Grid2D.Runtime;
using SnakeExample.Config;
using UnityEngine;
using Zenject;

namespace SnakeExample.Grid
{
    internal interface IGridViewHelper
    {
        Vector3 GetWorldPositionCenter(int x, int y);
        Vector3 GetMaxBoundaryPos();
        Vector3 GetMinBoundaryPos();
        float CellSize { get; }
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
                if (base.TrySetElement(x, y, element))
                {
                    element.GridPos = new Vector2Int(x, y);
                    element.IsOnGrid = true;
                    return true;
                }
            }
            Debug.LogError("Can't set element");
            return false;
        }

        public override bool TryRemoveElement(int x, int y)
        {
            var element = GetElement(x, y);
            if (element != null)
                element.IsOnGrid = false;
            
            return base.TryRemoveElement(x, y);
        }
    }
    internal class GridManager : IController, ISceneLifeCycleManaged, IGridViewHelper, IGridModelHelper
    {
        [Inject] private GameConfigDataSO _configData;
        private GridSystem _grid;
        public GridSystem Grid => _grid;

        public float CellSize { get; private set; }
        
        public void OnAwakeCallback()
        {
            CellSize = _configData.GridCellSize;
            _grid = new GridSystem(_configData.GridWidth, _configData.GridHeight, _configData.GridCellSize, 
                _configData.GridOriginPos, new VerticalConverter());
        }

        public void OnDestroyCallback()
        {
        }
        public Vector3 GetWorldPositionCenter(int x, int y) => _grid.GetWorldPositionCenter(x, y);

        public Vector3 GetMaxBoundaryPos() => _grid.GetWorldPositionCenter(_grid.Width, _grid.Height);

        public Vector3 GetMinBoundaryPos() => _grid.GetWorldPositionCenter(-1, -1);
    }
}
