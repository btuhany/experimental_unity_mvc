using UnityEngine;

namespace Batuhan.GridSystem.WorldGrid
{
    public interface IWorldGridDebuggable
    {
        bool IsReady { get; }
        int Width { get; }
        int Height { get; }
        Vector3 Forward { get; }
        Vector3 GetWorldPositionCenter(int x, int y);
        Vector3 GetWorldPosition(int x, int y);
        bool IsCellOccupied(int x, int y);
        string GetCellOccuppiedStr(int x, int y);
        Vector3 GetDebugDrawOffset();
    }
}
