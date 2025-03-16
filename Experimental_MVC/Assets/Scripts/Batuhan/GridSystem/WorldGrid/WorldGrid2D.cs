using Batuhan.GridSystem.Core;
using System;
using UnityEngine;

namespace Batuhan.GridSystem.WorldGrid
{
    public abstract class WorldGrid2D<T> : Grid2D<T>
    {
        private readonly float _cellSize;
        private readonly Vector3 _origin;
        private readonly IWorldCoordinateConverter _worldConverter;
        public readonly Vector3 Forward;
        public WorldGrid2D(int width, int height, float cellSize, Vector3 origin, IWorldCoordinateConverter worldConverter) : base(width, height)
        {
            _cellSize = cellSize;
            _origin = origin;
            _worldConverter = worldConverter ?? throw new ArgumentNullException(nameof(worldConverter), "World converter cannot be null.");
            Forward = _worldConverter.Forward;
        }
        public bool TrySetElementFromWorld(Vector3 worldPosition, T value)
        {
            Vector2Int cell = _worldConverter.WorldToCell(worldPosition, _cellSize, _origin);
            return TrySetElement(cell.x, cell.y, value);
        }

        public T GetElementFromWorld(Vector3 worldPosition)
        {
            Vector2Int cell = _worldConverter.WorldToCell(worldPosition, _cellSize, _origin);
            return GetElement(cell.x, cell.y);
        }

        public Vector2Int WorldToGridCoordinates(Vector3 worldPosition) => _worldConverter.WorldToCell(worldPosition, _cellSize, _origin);

        public Vector3 GetWorldPositionCenter(int x, int y) => _worldConverter.CellToWorldCenter(x, y, _cellSize, _origin);
        public Vector3 GetWorldPosition(int x, int y) => _worldConverter.CellToWorld(x, y, _cellSize, _origin);

        public Vector3 GetGridCenterWorldPosition()
        {
            Vector3 min = GetWorldPositionCenter(0, 0);
            Vector3 max = GetWorldPositionCenter(Width - 1, Height - 1);
            return (min + max) * 0.5f;
        }
    }
}
