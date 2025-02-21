using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using System;
using TimeCounter.Entities.InputHandler;
using TimeCounter.Events.CoreEvents;

namespace TimeCounter
{
    public class InputHandlerController : BaseControllerWithViewAndContext<IInputHandlerViewModel, IInputHandlerContext>, ILifeCycleHandler
    {
        private DisposableBag _disposableBag;
        public InputHandlerController(IInputHandlerViewModel model, IInputHandlerContext context) : base(model, context)
        {
        }

        public void OnAwakeCallback()
        {
            _view.StartStopCounterCommand.Subscribe(_ => OnStartStopCounterCommand()).AddTo(ref _disposableBag);
            _view.SpeedUpCommand.Subscribe(_ => OnSpeedUpCommand()).AddTo(ref _disposableBag);
            _view.SpeedDownCommand.Subscribe(_ => OnSpeedDownCommand()).AddTo(ref _disposableBag);

            _view.SetMaxCommand.Subscribe(_ => OnSetMaxCommand()).AddTo(ref _disposableBag);
            _view.SetMinCommand.Subscribe(_ => OnSetMinCommand()).AddTo(ref _disposableBag);
            _view.SetCountCommand.Subscribe(_ => OnSetCountCommand()).AddTo(ref _disposableBag);

            _view.SetMaxCountField.Subscribe(OnSetMaxCountFieldInput).AddTo(ref _disposableBag);
            _view.SetMinCountField.Subscribe(OnSetMinCountFieldInput).AddTo(ref _disposableBag);
            _view.SetCountField.Subscribe(OnSetCountFieldInput).AddTo(ref _disposableBag);

            _view.SetEnableSetMaxCountButton(false);
            _view.SetEnableSetMinCountButton(false);
            _view.SetEnableSetCountButton(false);
        }

        public void OnDestroyCallback()
        {
            _disposableBag.Dispose();
            _view.Dispose();
        }
        private void OnSpeedUpCommand()
        {
            _context.TickerModel.AddToTickSpeed(1);
        }
        private void OnSpeedDownCommand()
        {
            _context.TickerModel.AddToTickSpeed(-1);
        }
        private void OnSetMaxCommand()
        {
            var currentText = _view.SetMaxCountField.CurrentValue;
            if (int.TryParse(currentText, out int result))
            {
                _context.TickerModel.SetMaxTickCount(result);
            }
        }
        private void OnSetMinCommand()
        {
            var currentText = _view.SetMinCountField.CurrentValue;
            if (int.TryParse(currentText, out int result))
            {
                _context.TickerModel.SetMinTickCount(result);
            }
        }
        private void OnSetCountCommand()
        {
            var currentText = _view.SetCountField.CurrentValue;
            if (int.TryParse(currentText, out int result))
            {
                _context.TickerModel.TrySetTickCount(result);
            }
        }
        private void OnSetMaxCountFieldInput(string value)
        {
            if (int.TryParse(value, out int result))
            {
                _view.SetEnableSetMaxCountButton(true);
            }
            else
            {
                _view.SetEnableSetMaxCountButton(false);
            }
        }
        private void OnSetMinCountFieldInput(string value)
        {
            if (int.TryParse(value, out int result))
            {
                _view.SetEnableSetMinCountButton(true);
            }
            else
            {
                _view.SetEnableSetMinCountButton(false);
            }
        }
        private void OnSetCountFieldInput(string value)
        {
            if (int.TryParse(value, out int result))
            {
                _view.SetEnableSetCountButton(true);
            }
            else
            {
                _view.SetEnableSetCountButton(false);
            }
        }
        private void OnStartStopCounterCommand()
        {
            _context.EventBusCore.Publish(new StartStopCounterEvent());
            _view.HandleOnStartStopButtonView(_context.TickerModel.IsTickEnabled);
        }
    }
}
