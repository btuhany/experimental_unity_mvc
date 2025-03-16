using Batuhan.MVC.Core;
using R3;
using SnakeExample.Grid;
using UnityEngine;

namespace SnakeExample.Entities.Snake
{
    public class SnakeModel : IModel, IGridObject
    {
        private readonly ReactiveProperty<Vector2Int> _gridPos = new ReactiveProperty<Vector2Int>();

        public Vector2Int GridPos
        {
            get => _gridPos.Value;
            set => _gridPos.Value = value;
        }

        public ReadOnlyReactiveProperty<Vector2Int> GridPosReactive { get; }

        public SnakeModel()
        {
            GridPos = new Vector2Int(1, 2);
            GridPosReactive = _gridPos.ToReadOnlyReactiveProperty();
        }

        public void Dispose()
        {
            _gridPos?.Dispose();
            GridPosReactive?.Dispose();
        }
    }
}