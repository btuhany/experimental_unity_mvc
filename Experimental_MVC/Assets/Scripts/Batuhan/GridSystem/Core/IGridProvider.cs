using Batuhan.GridSystem.Core;

namespace Batuhan.GridSystem.Editor
{
    public interface IGridProvider<T>
    {
        Grid2D<T> GetGridModel();
    }
}
