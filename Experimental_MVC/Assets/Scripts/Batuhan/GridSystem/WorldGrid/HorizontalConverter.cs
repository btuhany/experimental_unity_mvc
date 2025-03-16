using UnityEngine;
namespace Batuhan.GridSystem.WorldGrid
{
    /// <summary>
    /// Row = negative Z-Axis 
    /// Col = positive X-Axis
    /// </summary>
    public class HorizontalConverter : IWorldCoordinateConverter
    {
        public Vector3 CellToWorldCenter(int col, int row, float cellSize, Vector3 origin)
        {
            return new Vector3(col * cellSize + cellSize * 0.5f, 0, -(row * cellSize + cellSize * 0.5f)) + origin;
        }

        public Vector3 CellToWorld(int col, int row, float cellSize, Vector3 origin)
        {
            return new Vector3(col, 0, -row) * cellSize + origin;
        }

        public Vector2Int WorldToCell(Vector3 worldPosition, float cellSize, Vector3 origin)
        {
            Vector3 gridPosition = (worldPosition - origin) / cellSize;
            var col = Mathf.FloorToInt(gridPosition.x);
            var row = Mathf.FloorToInt(-gridPosition.z);
            return new Vector2Int(col, row);
        }

        public Vector3 Forward => -Vector3.up;
    }
}

