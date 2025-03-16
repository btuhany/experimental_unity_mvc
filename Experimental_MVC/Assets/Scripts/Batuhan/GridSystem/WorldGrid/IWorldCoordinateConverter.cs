using UnityEngine;
namespace Batuhan.GridSystem.WorldGrid
{
    public interface IWorldCoordinateConverter
    {
        public abstract Vector3 CellToWorldCenter(int x, int y, float cellSize, Vector3 origin);
        public abstract Vector3 CellToWorld(int x, int y, float cellSize, Vector3 origin);
        public abstract Vector2Int WorldToCell(Vector3 worldPosition, float cellSize, Vector3 origin);
        public abstract Vector3 Forward { get; }
    }
}

