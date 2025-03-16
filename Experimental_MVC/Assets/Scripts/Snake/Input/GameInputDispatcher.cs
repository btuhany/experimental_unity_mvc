using SnakeExample.Events;
using System;
using UnityEngine;

namespace SnakeExample.Input
{
    public enum InputType
    {
        Any,
        Up,
        Down,
        Left,
        Right
    }
    //Simple basic input dispatcher and event source interfaces
    public interface IGameInputDispatcher
    {
        void DispatchInput(InputType input);
    }

    public interface ISnakeActionEventSource
    {
        Action<Vector2Int> OnMoveDirAction { get; set; }
    }
    public interface IGlobalInputActionEventSource
    {
        Action OnPressAnyAction { get; set; }
    }
    internal class GameInputDispatcher : IGameInputDispatcher, ISnakeActionEventSource, IGlobalInputActionEventSource
    {
        public Action<Vector2Int> OnMoveDirAction { get; set; }

        public Action OnPressAnyAction { get; set; }

        public void DispatchInput(InputType input)
        {
            OnPressAnyAction?.Invoke();
            HandleSnakeInputs(input);
        }
        private void HandleSnakeInputs(InputType input)
        {
            switch (input)
            {
                case InputType.Up:
                    OnMoveDirAction?.Invoke(Vector2Int.up);
                    break;
                case InputType.Down:
                    OnMoveDirAction?.Invoke(Vector2Int.down);
                    break;
                case InputType.Left:
                    OnMoveDirAction?.Invoke(Vector2Int.left);
                    break;
                case InputType.Right:
                    OnMoveDirAction?.Invoke(Vector2Int.right);
                    break;
                default:
                    break;
            }
        }
    }
}
