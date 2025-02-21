using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using TimeCounter.Entities.EntryPoint;
using TimeCounter.Events.GlobalEvents;
using Zenject;

namespace Assets.Scripts.TimeCounter.Entities.EntryPoint
{
    internal class EntryPointController : BaseController<IEntryPointContext>, ISceneLifeCycleManaged, IEntryPoint
    {
        public EntryPointController(IEntryPointContext context) : base(context)
        {
        }
        public void OnAwakeCallback()
        {
            _context.Debug.Log("Initialized!", this);
        }
        public void OnDestroyCallback()
        {
            _context.Debug.Log("Disposed!", this);
            Dispose();
        }
        public void OnStartCallback()
        {
            _context.EventBusGlobal.Publish(new SceneInitializedEvent());
        }
    }
}
