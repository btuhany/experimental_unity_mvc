using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using System.Diagnostics;
using TimeCounter.Entities.InputHandler;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace TimeCounter
{
    public class InputHandlerController : BaseControllerWithViewAndContext<IInputHandlerView, IInputHandlerContext>, ILifeCycleHandler
    {
        private DisposableBag _disposableBag;
        public InputHandlerController(IInputHandlerView model, IInputHandlerContext context) : base(model, context)
        {
        }

        public void OnAwakeCallback()
        {
            _view.SpeedUpCommand.Subscribe(_ => OnSpeedUpCommand()).AddTo(ref _disposableBag);
            _view.SpeedDownCommand.Subscribe(_ => OnSpeedDownCommand()).AddTo(ref _disposableBag);

            _view.SetMaxCommand.Subscribe(_ => OnSetMaxCommand()).AddTo(ref _disposableBag);
            _view.SetMinCommand.Subscribe(_ => OnSetMinCommand()).AddTo(ref _disposableBag);
            _view.SetCountCommand.Subscribe(_ => OnSetCountCommand()).AddTo(ref _disposableBag);

            _view.SetMaxCountField.Subscribe(OnSetMaxCountFieldInput).AddTo(ref _disposableBag);
            _view.SetMinCountField.Subscribe(OnSetMinCountFieldInput).AddTo(ref _disposableBag);
            _view.SetCountField.Subscribe(OnSetCountFieldInput).AddTo(ref _disposableBag);
        }
       
        public void OnDestroyCallback()
        {
            _disposableBag.Dispose();
            _view.Dispose();
        }
        private void OnSpeedUpCommand()
        {
            UnityEngine.Debug.Log("OnSpeedUpCommand");
        }
        private void OnSpeedDownCommand()
        {
            UnityEngine.Debug.Log("OnSpeedDownCommand");
        }
        private void OnSetMaxCommand()
        {
            UnityEngine.Debug.Log("OnSetMaxCommand");
        }
        private void OnSetMinCommand()
        {
            UnityEngine.Debug.Log("OnSetMinCommand");
        }
        private void OnSetCountCommand()
        {
            UnityEngine.Debug.Log("OnSetCountCommand");
        }
        private void OnSetMaxCountFieldInput(string value)
        {
            UnityEngine.Debug.Log("On Set Max Count Field Input: " + value);
        }
        private void OnSetMinCountFieldInput(string value)
        {
            UnityEngine.Debug.Log("OnSetMinCountFieldInput: " + value);
        }
        private void OnSetCountFieldInput(string value)
        {
            UnityEngine.Debug.Log("OnSetCountFieldInput: " + value);
        }
    }
}
