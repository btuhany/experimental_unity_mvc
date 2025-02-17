using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using TimeCounter.Entities.EntryPoint;
using TimeCounter.Events.GlobalEvents;
using Zenject;

namespace Assets.Scripts.TimeCounter.Entities.EntryPoint
{
    internal class EntryPointController : BaseController<IEntryPointContext>, ILifeCycleHandler, IEntryPoint
    {
        public EntryPointController(IEntryPointContext context) : base(context)
        {
        }
        public void Initialize()
        {
            _context.Debug.Log("Initialized!", this);
        }
        public void Dispose()
        {
            _context.Debug.Log("Disposed!", this);
        }
        public void Start()
        {
            _context.EventBusGlobal.Publish(new SceneInitializedEvent());
        }
    }
}
