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
        public Vector2Int Direction { get; private set; }
        public int Speed { get; private set; }
        public Action OnStop;
        public Vector2Int GridPos
        {
            get => _gridPos.Value;
            set => _gridPos.Value = value;
        }

        public bool IsOnGrid { get; set; }

        public GridObjectType ObjectType => GridObjectType.Snake;

        public ReadOnlyReactiveProperty<Vector2Int> GridPosReactive { get; }

        public SnakeModel(GameConfigDataSO configDataSO)
        {
            Direction = configDataSO.SnakeStartDir;
            Speed = configDataSO.SnakeSpeed;
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

        public void Move(Vector2Int nextPos)
        {
            _gridModel.Grid.TryRemoveElement(GridPos.x, GridPos.y);
            var isSet = _gridModel.Grid.TrySetElement(nextPos.x, nextPos.y, this);
            
            if (!isSet)
            {
                Debug.LogError($"SnakeModel.TryUpdateGridPos: Failed to set element at {GridPos}");
                Speed = 0;
                Direction = Vector2Int.zero;
                OnStop?.Invoke();
            }
        }
        internal void OnMoveDirAction(Vector2Int dir)
        {
            Direction = dir;
        }
    }
}