using Batuhan.MVC.Core;
using R3;
using SnakeExample.Config;
using SnakeExample.Grid;
using System;
using SnakeExample.Tick;
using UnityEngine;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    public class SnakeModel : IModel, IGridObject
    {
        [Inject] private GameData _gameData;
        [Inject] private IGridModelHelper _gridModel;
        private readonly ReactiveProperty<Vector2Int> _gridPos = new ReactiveProperty<Vector2Int>();
        public Vector2Int Direction { get; private set; }
        public float SpeedAdditionOnEat { get; private set; }
        public Action OnStop;
        public Vector2Int GridPos
        {
            get => _gridPos.Value;
            set => _gridPos.Value = value;
        }

        public bool IsOnGrid { get; set; }

        public GridObjectType ObjectType => GridObjectType.Snake;
        public void OnRemovedFromGrid()
        {
            
        }

        public ReadOnlyReactiveProperty<Vector2Int> GridPosReactive { get; }

        public SnakeModel(GameConfigDataSO configDataSO)
        {
            Direction = configDataSO.SnakeStartDir;
            SpeedAdditionOnEat = configDataSO.SnakeSpeedAddition;
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
            bool isFoodEaten = false;
            
            var element = _gridModel.Grid.GetElement(nextPos.x, nextPos.y);
            if (element != null)
            {
                if (element.ObjectType == GridObjectType.Food)
                {
                    isFoodEaten = true;
                    _gridModel.Grid.TryRemoveElement(element.GridPos.x, element.GridPos.y);
                }
            }
            
            
            _gridModel.Grid.TryRemoveElement(GridPos.x, GridPos.y);
            var isSet = _gridModel.Grid.TrySetElement(nextPos.x, nextPos.y, this);
            
            if (!isSet)
            {
                Debug.LogError($"SnakeModel.TryUpdateGridPos: Failed to set element at {GridPos}");
                SpeedAdditionOnEat = 0;
                Direction = Vector2Int.zero;
                OnStop?.Invoke();
            }
            else if (isFoodEaten)
            {
                ProcessFood();
            }
        }

        private void ProcessFood()
        {
            _gameData.TickSpeedDivider += SpeedAdditionOnEat;
        }

        internal void OnMoveDirAction(Vector2Int dir)
        {
            Direction = dir;
        }
    }
}