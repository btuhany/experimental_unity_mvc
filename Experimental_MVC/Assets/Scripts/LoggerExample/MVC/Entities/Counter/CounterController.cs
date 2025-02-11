using Assets.Scripts.Batuhan.Core.MVC.Base;
using Assets.Scripts.LoggerExample.MVC.Entities.Circle;
using Batuhan.Core.MVC;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine; //TODOBY: Prevent Unity Engine Dependency
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterController : BaseController<CounterModel, CounterView>, Zenject.IInitializable, IDisposable //TODOby: IDisposable, Destroying object
    {
        //TEMP
        [Inject]
        CircleController.Factory _circleFactory;

        private ICounterContext _context;
        public override IContext Context => _context;

        //TODOby: A larger scope of a context needed instead of counter context but its okay for now
        [Inject]
        public CounterController(CounterModel model, CounterView view, ICounterContext context) : base(model, view)
        {
            _context = context;
        }

        public override void Initialize()
        {
            if (!_isInitialized)
            {
                _model.Initialize();
                _view.Initialize();
                //TODOBY: MVC Entity Initialized and Ready to Use Event
                _model.OnCountValueChanged += OnCountValueChanged;  //TODO Observer Pattern
                ActivateTick().Forget();
            }
        }
        //}

        public void Dispose()
        {
            _model.OnCountValueChanged -= OnCountValueChanged;
            Debug.Log("CounterController Disposed");
        }

        private async UniTaskVoid ActivateTick()
        {
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
            //TODO Command or Event Manager via Context
            _view.OnCountChanged(counterValue);
        }

    }
}
