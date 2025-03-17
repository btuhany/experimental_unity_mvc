using SnakeExample.Grid;
using UnityEngine;
using Zenject;

namespace SnakeExample.View
{
    [RequireComponent(typeof(Camera))]
    public class CameraViewHelper : MonoBehaviour
    {
        [Inject] private GridManager _gridManager;

        private void Start()
        {
            var gridPos = _gridManager.Grid.GetGridCenterWorldPosition();
            transform.position = new Vector3(gridPos.x, gridPos.y, transform.position.z);
        }
    }
}
