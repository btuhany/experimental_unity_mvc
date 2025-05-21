using Batuhan.MVC.Core;
using R3;
using SnakeExample.Config;
using SnakeExample.Grid;
using System;
using System.Collections.Generic;
using SnakeExample.Tick;
using UnityEngine;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    public class SnakeTailModel : IGridObject, IModel
    {
        public Vector2Int GridPos { get; set; }
        public bool IsOnGrid { get; set; }
        public GridObjectType ObjectType => GridObjectType.SnakeTail;
        public void OnRemovedFromGrid()
        {
        }

        public void Dispose()
        {
            
        }
    }
    public class SnakeModel : IModel, IGridObject
    {
        private List<SnakeTailModel> _tails = new();
        [Inject] private GameData _gameData;
        [Inject] private IGridModelHelper _gridModel;
        private readonly ReactiveProperty<Vector2Int> _gridPos = new ReactiveProperty<Vector2Int>();
        private readonly ReactiveProperty<int> _tailSize  = new ReactiveProperty<int>();
        public Vector2Int Direction { get; private set; }
        public Vector2Int StartPos { get; private set; }
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
        public ReadOnlyReactiveProperty<int> TailSize { get; private set;}

        public SnakeModel(GameConfigDataSO configDataSO)
        {
            Direction = configDataSO.SnakeStartDir;
            SpeedAdditionOnEat = configDataSO.SnakeSpeedAddition;
            StartPos = GridPos = configDataSO.SnakeStartPos;
            GridPosReactive = _gridPos.ToReadOnlyReactiveProperty();

            _tailSize.Value = 0;
            TailSize = _tailSize.ToReadOnlyReactiveProperty();
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
                    _gridModel.Grid.TryRemoveElement(element.GridPos.x, element.GridPos.y);  //triggers food eaten callback
                }
                // else if (element.ObjectType == GridObjectType.SnakeTail)
                // {
                //     StopMovement();
                // }
            }
            
            var moveResult = MoveInternal(nextPos);
            if (!moveResult)
            {
                StopMovement();
            }
            else if (isFoodEaten)
            {
                ProcessFood();
            }
        }

        private bool MoveInternal(Vector2Int nextPos)
        {
            Vector2Int previousPosition = GridPos;
            _gridModel.Grid.TryRemoveElement(GridPos.x, GridPos.y);
            var isSet = _gridModel.Grid.TrySetElement(nextPos.x, nextPos.y, this);
            
            for (int i = 0; i < _tails.Count; i++)
            {
                var tail = _tails[i];
                Vector2Int temp = tail.GridPos;
                _gridModel.Grid.TryRemoveElement(temp.x, temp.y);
                _gridModel.Grid.TrySetElement(previousPosition.x, previousPosition.y, tail);
                previousPosition = temp;
            }

            return isSet;
        }

        private void StopMovement()
        {
            SpeedAdditionOnEat = 0;
            Direction = Vector2Int.zero;
            OnStop?.Invoke();
        }

        private void ProcessFood()
        {
            _tailSize.Value++;
            var newTail = new SnakeTailModel();
            _tails.Add(newTail);
            _gameData.TickSpeedDivider += SpeedAdditionOnEat;
        }
        internal void OnMoveDirAction(Vector2Int dir)
        {
            Direction = dir;
        }

        internal void OnRestart()
        {
            for (int i = 0; i < _tails.Count; i++)
            {
                _gridModel.Grid.TryRemoveElement(_tails[i].GridPos.x, _tails[i].GridPos.y);
            }
            _tails.Clear();
            _tailSize.Value = 0;
            _gameData.Restart();
            GridPos = StartPos;
        }
    }
}