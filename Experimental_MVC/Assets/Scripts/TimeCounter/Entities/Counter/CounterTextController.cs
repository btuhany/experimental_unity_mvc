using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TimeCounter.Commands;
using TimeCounter.Entities.CountIndicator;
using TimeCounter.Events.GlobalEvents;
using TimeCounter.Events.ModelEvents;
using Zenject;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextController : BaseControllerWithoutContext<CounterTextModel, CounterTextView>, Zenject.IInitializable, IDisposable
    {
        //TEMP
        [Inject]
        CountIndicatorController.Factory _circleFactory;

        private ICounterTextContext _context;
        public override IContext Context => _context;

        private CancellationToken _tickCancellationToken;

        [Inject]
        public CounterTextController(CounterTextModel model, CounterTextView view, ICounterTextContext context) : base(model, view)
        {
            _context = context;
            _tickCancellationToken = new CancellationToken();
        }

        //CALLED FROM ZENJECT 
        public override void Initialize()
        {
            _context.Debug.Log("Initialized!", this);
            if (!_isInitialized)
            {
                _model.Setup(_context);
                _view.Setup(_context);
                SubscribeEvents();
            }
        }

        //CALLED FROM ZENJECT
        public void Dispose()
        {
            _context.Debug.Log("Disposed!", this);
            _model.Dispose();
            _view.Dispose();
            UnsubscribeEvents();
        }
        private void SubscribeEvents()
        {
            _context.EventBusModel.Subscribe<CountValueUpdatedEvent>(OnCountValueUpdated);
            _context.EventBusGlobal.Subscribe<AppInitializedEvent>(HandleOnAppInitialized);
        }
        private void UnsubscribeEvents()
        {
            _context.EventBusGlobal.Unsubscribe<AppInitializedEvent>(HandleOnAppInitialized);
            _context.EventBusModel.Unsubscribe<CountValueUpdatedEvent>(OnCountValueUpdated);
        }
        private void HandleOnAppInitialized(AppInitializedEvent @event)
        {
        }

        private void OnCountValueUpdated(CountValueUpdatedEvent @event)
        {
            _context.CommandManager.ExecuteCommand(new UpdateCounterTextCommand(@event.NewValue));
        }
        private async UniTask ActivateTick()
        {
            await UniTask.WaitForSeconds(1.0f, cancellationToken: _tickCancellationToken);
            while (true)
            {
                _tickCancellationToken.ThrowIfCancellationRequested();

                var secondsToWait = (float)(1f / _model.CountSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000), cancellationToken: _tickCancellationToken);
                _model.IncreaseCounter();
                //var circleController = _circleFactory.Create();
                //circleController.Initialize();
                _context.Debug.Log("tick!", this);
            }
        }
    }
}
