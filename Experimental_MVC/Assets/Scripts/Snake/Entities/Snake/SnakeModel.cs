using Batuhan.MVC.Core;
using R3;
using SnakeExample.Config;
using SnakeExample.Grid;
using System;
using UnityEngine;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    public class SnakeModel : IModel, IGridObject
    {
        [Inject] private IGridModelHelper _gridModel;
        private readonly ReactiveProperty<Vector2Int> _gridPos = new ReactiveProperty<Vector2Int>();
        private Vector2Int _direction;
        private int _speed;
        public Action OnStop;
        public Vector2Int GridPos
        {
            get => _gridPos.Value;
            set => _gridPos.Value = value;
        }

        public ReadOnlyReactiveProperty<Vector2Int> GridPosReactive { get; }

        public SnakeModel(GameConfigDataSO configDataSO)
        {
            _direction = configDataSO.SnakeStartDir;
            _speed = configDataSO.SnakeSpeed;
            GridPos = configDataSO.SnakeStartPos;
            GridPosReactive = _gridPos.ToReadOnlyReactiveProperty();
        }
        public void Initialize(Action onStopCallback)
        {
            OnStop = onStopCallback;
            _gridModel.Grid.TrySetElement(GridPos.x, GridPos.y, this);
        }
        public void Dispose()
        {
            _gridPos?.Dispose();
            GridPosReactive?.Dispose();
        }

        public void OnTick()
        {
            var nextPos = GridPos + _direction * _speed;
            if (_gridModel.Grid.TrySetElement(nextPos.x, nextPos.y, this))
            {
                _gridModel.Grid.TryRemoveElement(GridPos.x, GridPos.y);
                GridPos = nextPos;
            }
            else
            {
                Debug.LogError($"SnakeModel.TryUpdateGridPos: Failed to set element at {GridPos}");
                _speed = 0;
                _direction = Vector2Int.zero;
                OnStop?.Invoke();
            }
        }
        internal void OnMoveDirAction(Vector2Int dir)
        {
            _direction = dir;
        }
    }
}