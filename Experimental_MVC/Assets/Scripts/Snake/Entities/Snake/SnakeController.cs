using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using SnakeExample.Events;
using System;
using UnityEngine;
using Zenject;

namespace SnakeExample.Entities.Snake
{
    internal class SnakeController : BaseController<SnakeModel, SnakeView, SnakeContext>, ISceneLifeCycleManaged
    {
        private readonly CompositeDisposable _disposableBag = new CompositeDisposable();
        public SnakeController(SnakeModel model, SnakeView view, SnakeContext context) : base(model, view, context)
        {
            
        }

        public void OnAwakeCallback()
        {
            _context.InputEventSource.OnMoveDirAction += _model.OnMoveDirAction;
            _context.EventBus.Subscribe<TickEvent>(OnTick);
            Initialize();
        }

        public void OnDestroyCallback()
        {
            _context.InputEventSource.OnMoveDirAction -= _model.OnMoveDirAction;
            _context.EventBus.Unsubscribe<TickEvent>(OnTick);
            _disposableBag.Dispose();
        }
        private void OnTick(TickEvent @event)
        {
            _model.OnTick();
        }
        private void Initialize()
        {
            _model.Initialize();
            _model.GridPosReactive.Subscribe(_view.OnGridPosUpdated).AddTo(_disposableBag);
        }
    }
}