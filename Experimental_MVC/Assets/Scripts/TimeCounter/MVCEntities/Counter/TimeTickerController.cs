using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
using System.Threading;
using TimeCounter.Events.CoreEvents;
using TimeCounter.Events.GlobalEvents;

namespace TimeCounter.Entities.Counter
{
    public class TimeTickerController : BaseControllerWithModelAndContext<ITimeTickerModel, ITimeTickerContext>, ILifeCycleHandler
    {
        private CancellationTokenSource _tickCancellationTokenSource;
        public TimeTickerController(ITimeTickerModel model, ITimeTickerContext context) : base(model, context)
        {
            _tickCancellationTokenSource = new CancellationTokenSource();
        }
        public void Initialize()
        {
            _context.EventBusGlobal.Subscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _model.OnCountValueChanged += HandleOnCountValueChanged;
        }
        public void Dispose()
        {
            _context.EventBusGlobal.Unsubscribe<SceneInitializedEvent>(HandleOnSceneInitialized);
            _model.OnCountValueChanged -= HandleOnCountValueChanged;
            DeactivateTick();
        }

        private void HandleOnCountValueChanged(int newValue)
        {
            _context.EventBusCore.Publish(new TimeCountValueUpdatedEvent() { UpdatedValue = newValue });
        }

        private void HandleOnSceneInitialized(SceneInitializedEvent @event)
        {
            _context.Debug.Log("Handle on scene init", this);
            ActivateTick().Forget();
        }
        private void HandleOnTick()
        {
            _model.IncreaseCounter(1);
            
            _context.Debug.Log("tick!", this);
        }
        private async UniTask ActivateTick()
        {
            while (true)
            {
                var countSpeed = UnityEngine.Mathf.Max(_model.CountSpeed, 0.01f);
                var secondsToWait = (float)(1f / countSpeed);
                await UniTask.Delay((int)(secondsToWait * 1000), cancellationToken: _tickCancellationTokenSource.Token);

                if (_tickCancellationTokenSource.Token.IsCancellationRequested)
                    break;

                HandleOnTick();
            }
        }
        private void DeactivateTick()
        {
            _tickCancellationTokenSource?.Cancel();
            _tickCancellationTokenSource?.Dispose();
            _tickCancellationTokenSource = null;
        }
    }
}
