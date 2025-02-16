using Batuhan.MVC.Core;
using TimeCounter.Entities.EntryPoint;
using TimeCounter.Events.GlobalEvents;
using Zenject;

namespace Assets.Scripts.TimeCounter.Entities.EntryPoint
{
    internal class EntryPointController : ILifeCycleHandler, IEntryPoint
    {
        private IEntryPointContext _context;
        [Inject]
        public EntryPointController(IEntryPointContext context)
        {
            _context = context;
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
