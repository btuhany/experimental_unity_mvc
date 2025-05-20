using Batuhan.MVC.UnityComponents.Base;
using System;
using UnityEngine;

namespace SnakeExample.Entities.ObstacleManager
{
    internal class BoundaryManagerView : BaseViewMonoBehaviour
    {
        [SerializeField] private GameObject _boundaryPrefab;
        public override Type ContractTypeToBind => typeof(BoundaryManagerView);

        public void ConstructBoundaryObstacles(Vector3 maxPos, Vector3 minPos, float cellSize)
        {
            float scaleX = maxPos.x - minPos.x;
            var lower = Instantiate(_boundaryPrefab, transform);
            lower.transform.localScale = new Vector3(scaleX, cellSize, 1);
            lower.transform.position = new Vector3((maxPos.x - minPos.x - cellSize) / 2f, minPos.y, 0);

            var upper = Instantiate(_boundaryPrefab, transform);
            upper.transform.localScale = new Vector3(scaleX, cellSize, 1);
            upper.transform.position = new Vector3((maxPos.x - minPos.x - cellSize) / 2f, maxPos.y, 0);

            float scaleY = maxPos.y - minPos.y;
            var right = Instantiate(_boundaryPrefab, transform);
            right.transform.localScale = new Vector3(cellSize, scaleY, 1);
            right.transform.position = new Vector3(maxPos.x, (maxPos.y - minPos.y - cellSize) / 2f, 0);

            var left = Instantiate(_boundaryPrefab, transform);
            left.transform.localScale = new Vector3(cellSize, scaleY, 1);
            left.transform.position = new Vector3(minPos.x, (maxPos.y - minPos.y - cellSize) / 2f, 0);
        }
    }
}
