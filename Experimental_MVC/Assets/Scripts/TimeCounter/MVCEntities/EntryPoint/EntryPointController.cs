using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using TimeCounter.Events.GlobalEvents;

namespace TimeCounter.Entities.EntryPoint
{
    internal class EntryPointController : BaseController<IEntryPointContext>, ISceneLifeCycleManaged, IEntryPoint
    {
        public EntryPointController(IEntryPointContext context) : base(context)
        {
        }
        public void OnAwakeCallback()
        {
        }
        public void OnDestroyCallback()
        {
            Dispose();
        }
        public void OnStartCallback()
        {
            _context.EventBusGlobal.Publish(new SceneInitializedEvent());
        }
    }
}
