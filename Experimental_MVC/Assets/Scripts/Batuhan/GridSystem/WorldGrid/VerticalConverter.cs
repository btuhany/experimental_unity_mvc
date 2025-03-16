using UnityEngine;

namespace Batuhan.GridSystem.WorldGrid
{
    /// <summary>
    /// Row = positive Y-Axis 
    /// Col = positive X-Axis
    /// </summary>
    public class VerticalConverter : IWorldCoordinateConverter
    {
        public Vector3 Forward => Vector3.forward;

        public Vector3 CellToWorld(int x, int y, float cellSize, Vector3 origin)
        {
            return new Vector3(x, y, 0) * cellSize + origin;
        }

        public Vector3 CellToWorldCenter(int x, int y, float cellSize, Vector3 origin)
        {
            return new Vector3(x * cellSize + cellSize * 0.5f, y * cellSize + cellSize * 0.5f, 0) + origin;
        }

        public Vector2Int WorldToCell(Vector3 worldPosition, float cellSize, Vector3 origin)
        {
            Vector3 gridPosition = (worldPosition - origin) / cellSize;
            var x = Mathf.FloorToInt(gridPosition.x);
            var y = Mathf.FloorToInt(gridPosition.y);
            return new Vector2Int(x, y);
        }
    }
}
