using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using TimeCounter.Commands;
using TimeCounter.Entities.CountIndicator;
using TimeCounter.Events.Global;
using UnityEngine; //TODOBY: Prevent Unity Engine Dependency
using Zenject;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextController : BaseControllerWithoutContext<CounterTextModel, CounterTextView>, Zenject.IInitializable, IDisposable //TODOby: IDisposable, Destroying object
    {
        //TEMP
        [Inject]
        CountIndicatorController.Factory _circleFactory;

        private ICounterTextContext _context;
        public override IContext Context => _context;

        [Inject]
        public CounterTextController(CounterTextModel model, CounterTextView view, ICounterTextContext context) : base(model, view)
        {
            _context = context;
        }

        public override void Initialize()
        {
            if (!_isInitialized)
            {
                _model.Initialize();
                _view.Initialize(_context);
                //TODOBY: MVC Entity Initialized and Ready to Use Event
                _model.OnCountValueChanged += OnCountValueChanged;  //TODO Observer Pattern
                _context.EventBusGlobal.Subscribe<AppInitializedEvent>(HandleOnAppInitialized);
            }
        }

        private void HandleOnAppInitialized(AppInitializedEvent eventData)
        {
            ActivateTick().Forget();
        }

        public void Dispose()
        {
            _context.EventBusGlobal.Unsubscribe<AppInitializedEvent>(HandleOnAppInitialized);
            _model.OnCountValueChanged -= OnCountValueChanged;
            Debug.Log("CounterController Disposed");
        }

        private async UniTaskVoid ActivateTick()
        {
            UnityEngine.Debug.Log("Started Ticking...");
            await UniTask.WaitForSeconds(1.0f);
            while (true)
            {
                var secondsToWait = (float)(1f / _model.CountSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000));
                _model.IncreaseCounter();
                var circleController = _circleFactory.Create();
                circleController.Initialize();
            }
        }

        private IEnumerator CounterCoroutine()
        {
            while (true)
            {
                var secondsToWait = (float) (1f / _model.CountSpeed);
                yield return new WaitForSeconds(secondsToWait);
                _model.IncreaseCounter();
            }
        }


        private void OnCountValueChanged(int counterValue)
        {
            //SEND UPDATE COUNT TEXT
            //_view.OnCountChanged(counterValue);
            _context.CommandManager.ExecuteCommand(new UpdateCounterTextCommand(counterValue));
            if (_model.CounterValue == 7)
            {
                _view.RemoveTextCommandListener();
            }
        }

    }
}
