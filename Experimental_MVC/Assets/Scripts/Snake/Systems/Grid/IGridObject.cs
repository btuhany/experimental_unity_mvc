using UnityEngine;

namespace SnakeExample.Grid
{
    internal interface IGridObject
    {
        Vector2Int GridPos { get; set; }
    }
}
