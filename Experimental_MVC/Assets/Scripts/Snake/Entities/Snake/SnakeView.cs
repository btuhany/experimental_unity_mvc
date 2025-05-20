using Batuhan.MVC.UnityComponents.Base;
using SnakeExample.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    internal class SnakeView : BaseViewMonoBehaviour
    {
        [SerializeField] private SnakeTailView _snakeTailView;
        [Inject] private IGridViewHelper _gridViewHelper;
        private List<SnakeTailView> _tails = new List<SnakeTailView>();
        public override Type ContractTypeToBind => typeof(SnakeView);

        public void OnTailSizeChanged(int size)
        {
            var diff = size - _tails.Count;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    var newTail = Instantiate(_snakeTailView);
                    newTail.transform.position = transform.position;
                    _tails.Add(newTail);
                }
            }
        }

        public void OnGridPosUpdated(Vector2Int newPos)
        {
            Vector3 previousPosition = transform.position;
            transform.position = _gridViewHelper.GetWorldPositionCenter(newPos.x, newPos.y);
            for (int i = 0; i < _tails.Count; i++)
            {
                var tail = _tails[i];
                Vector3 temp = tail.transform.position;
                tail.transform.position = previousPosition;
                previousPosition = temp;
            }
        }

        public void OnRestart()
        {
            foreach (var view in _tails)
            {
                Destroy(view.gameObject);
            }
            _tails.Clear();
        }
    }
}