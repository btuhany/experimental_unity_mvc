using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using ExperimentalMVC.SnakeExample;
using UnityEngine.InputSystem;

namespace SnakeExample.Entities.InputReader
{
    public class InputReaderController : BaseController<IInputReaderContext>, ISceneLifeCycleManaged, SnakeExampleInputActions.ISnakeSceneActions
    {
        private SnakeExampleInputActions _inputActions;
        private SnakeExampleInputActions.SnakeSceneActions _snakeSceneActions;
        public InputReaderController(IInputReaderContext context) : base(context)
        {
        }

        public void OnAwakeCallback()
        {
            _inputActions = new SnakeExampleInputActions();
            _snakeSceneActions = _inputActions.SnakeScene;
            _snakeSceneActions.AddCallbacks(this);

            _snakeSceneActions.Enable();
        }

        public void OnDestroyCallback()
        {
            _snakeSceneActions.Disable();
            _inputActions.Dispose();
        }

        public void OnMoveUp(InputAction.CallbackContext context)
        {
            if (context.performed)
                UnityEngine.Debug.Log("Move Up performed");
        }
    }
}
