using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using R3;
using SnakeExample.Entities.GameManager;
using SnakeExample.Events;
using UnityEngine;

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
            _context.EventBus.Subscribe<SceneInitializationEvent>(OnSceneInitializationComplete);
            _context.EventBus.Subscribe<GameStateChanged>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChanged obj)
        {
            if (obj.NewState == GameState.RestartDelay)
            {
                _model.OnRestart();
                _view.OnRestart();
            }
        }

        public void OnDestroyCallback()
        {
            _context.EventBus.Unsubscribe<SceneInitializationEvent>(OnSceneInitializationComplete);
            _context.EventBus.Unsubscribe<TickEvent>(OnTick);
            _context.EventBus.Unsubscribe<GameStateChanged>(OnGameStateChanged);
            _context.InputEventSource.OnMoveDirAction -= _model.OnMoveDirAction;
            _disposableBag.Dispose();
        }
        private void OnSceneInitializationComplete(SceneInitializationEvent obj)
        {
            _model.Initialize(OnStop);
            _model.GridPosReactive.Subscribe(_view.OnGridPosUpdated).AddTo(_disposableBag);
            _model.TailSize.Subscribe(_view.OnTailSizeChanged).AddTo(_disposableBag);
        }
        private void OnTick(TickEvent @event)
        {
            Move();
        }
        private void Move()
        {
            var nextPos = _model.GridPos + _model.Direction;
            _model.Move(nextPos);
        }
        private void OnStop()
        {
            _context.EventBus.Publish(new SnakeStoppedEvent());
        }
    }
}