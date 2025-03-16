using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using UnityEngine;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    internal class SnakeController : BaseController<SnakeModel, SnakeView, SnakeContext>, ISceneLifeCycleManaged
    {
        [Inject] private Grid.GridManager _gridManager;
        private readonly CompositeDisposable _disposableBag = new CompositeDisposable();
        public SnakeController(SnakeModel model, SnakeView view, SnakeContext context) : base(model, view, context)
        {
            
        }

        public void OnAwakeCallback()
        {
            Initialize();
        }

        public void OnDestroyCallback()
        {
            _disposableBag.Dispose();
        }

        private void Initialize()
        {
            _model.GridPosReactive.Subscribe(_view.OnGridPosUpdated).AddTo(_disposableBag);
        }
    }
}