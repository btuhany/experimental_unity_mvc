using Batuhan.MVC.Base;
using Cysharp.Threading.Tasks;
using TimeCounter.Events.GlobalEvents;
using Zenject;

namespace TimeCounter.Entities.AppInitializer
{
    internal class AppInitializerController : BaseController<AppInitializerModel, IAppInitializerContext>, Zenject.IInitializable
    {
        [Inject]
        public AppInitializerController(AppInitializerModel model, IAppInitializerContext context) : base(model, context)
        {
        }

        public override void Initialize()
        {
            _context.EventBusGlobal.Publish(new AppInitializedEvent() { Time = UnityEngine.Time.time });
        }
    }
}
