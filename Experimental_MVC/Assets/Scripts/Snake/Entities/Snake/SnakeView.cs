using Batuhan.MVC.UnityComponents.Base;
using SnakeExample.Grid;
using System;
using UnityEngine;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    internal class SnakeView : BaseViewMonoBehaviour
    {
        [Inject] private IGridViewHelper _gridViewHelper;
        public override Type ContractTypeToBind => typeof(SnakeView);
        public void OnGridPosUpdated(Vector2Int newPos)
        {
            Debug.Log($"SnakeView.OnGridPosUpdated: {newPos}");
            transform.position = _gridViewHelper.GetWorldPositionCenter(newPos.x, newPos.y);
        }
    }
}