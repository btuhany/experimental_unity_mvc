using Batuhan.EventBus;
using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using ExperimentalMVC.SnakeExample;
using SnakeExample.Events;
using SnakeExample.Input;
using UnityEngine.InputSystem;
using Zenject;

namespace SnakeExample.Entities.InputReader
{
    public class InputReaderController : BaseController<IInputReaderContext>, ISceneLifeCycleManaged, SnakeExampleInputActions.ISnakeSceneActions
    {
        [Inject] private IGameInputDispatcher _inputDispatcher;
        private bool _canRead;
        private SnakeExampleInputActions _inputActions;
        private SnakeExampleInputActions.SnakeSceneActions _snakeSceneActions;
        public InputReaderController(IInputReaderContext context) : base(context)
        {
            _canRead = false;
        }

        public void OnAwakeCallback()
        {
            _inputActions = new SnakeExampleInputActions();
            _snakeSceneActions = _inputActions.SnakeScene;
            _snakeSceneActions.AddCallbacks(this);
            _snakeSceneActions.Enable();
            _context.EventBus.Subscribe<GameStateChanged>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChanged changed)
        {
            _canRead = changed.NewState == GameManager.GameState.Started;
        }

        public void OnDestroyCallback()
        {
            _snakeSceneActions.Disable();
            _inputActions.Dispose();
            _context.EventBus.Unsubscribe<GameStateChanged>(OnGameStateChanged);
        }

        public void OnMoveUp(InputAction.CallbackContext context)
        {
            if (context.performed)
                _inputDispatcher.DispatchInput(InputType.Up);
        }

        public void OnAny(InputAction.CallbackContext context)
        {
            if (context.performed)
                _inputDispatcher.DispatchInput(InputType.Any);
        }

        public void OnMoveDown(InputAction.CallbackContext context)
        {
            if (context.performed)
                _inputDispatcher.DispatchInput(InputType.Down);
        }

        public void OnMoveLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
                _inputDispatcher.DispatchInput(InputType.Left);
        }

        public void OnMoveRight(InputAction.CallbackContext context)
        {
            if (context.performed)
                _inputDispatcher.DispatchInput(InputType.Right);
        }
    }
}
