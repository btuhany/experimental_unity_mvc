using UnityEngine;

namespace SnakeExample.Grid
{
    public enum GridObjectType
    {
        None = 0,
        Snake = 1,
        Obstacle = 2,
        Food = 3,
        SnakeTail = 4,
    }
    public interface IGridObject
    {
        Vector2Int GridPos { get; set; }
        bool IsOnGrid { get; set; }
        GridObjectType ObjectType { get; }
        void OnRemovedFromGrid();
    }
}
