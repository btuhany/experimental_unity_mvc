using BatuhanYigit.Grid2D.Runtime;
using SnakeExample.Grid;
using UnityEngine;
using Zenject;

namespace SnakeExample.Editor
{
    public class GridModelDebugProvider : MonoBehaviour, IGridProvider<IGridObject>
    {
        [Inject] private IGridModelHelper _gridModel;
        public Grid2D<IGridObject> GetGridModel()
        {
            return _gridModel.Grid;
        }
    }
}