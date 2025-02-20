using Assets.Scripts.TimeCounter.Commands;
using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using System;
using TimeCounter.Commands;
using TimeCounter.Data;
using Zenject;

namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorController : BaseController<CountIndicatorModel, IViewContextual<ICountIndicatorContext>, ICountIndicatorContext>, IDisposable
    {
        internal class Factory : PlaceholderFactory<CountIndicatorController> { }

        public CountIndicatorController(CountIndicatorModel model, IViewContextual<ICountIndicatorContext> view, ICountIndicatorContext context) : base(model, view, context)
        {
        }

        public void Initialize(CountIndicatorInitData initData)
        {
            _view.Setup(_context);
            _model.Setup(_context);
            _model.SetInitialData(initData.CommonData);

            _context.CommandManager.ExecuteCommand(new SetParentCommand() { Parent = initData.ParentTransform });
        }
        
        public void DestroyEntityForRuntime()
        {
            _context.CommandManager.ExecuteCommand(new DestroyGameObjectCommand());
            Dispose();
        }
    }
}
