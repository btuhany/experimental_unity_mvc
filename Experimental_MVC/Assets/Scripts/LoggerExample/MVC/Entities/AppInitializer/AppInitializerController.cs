using Assets.Scripts.Batuhan.Core.MVC.Base;
using Batuhan.Core.MVC;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Events.Category;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.AppInitializer
{
    internal class AppInitializerController : BaseController<AppInitializerModel, AppInitializerView>, Zenject.IInitializable
    {
        public override IContext Context => _context;
        private IAppInitializerContext _context;
        
        [Inject]
        public AppInitializerController(AppInitializerModel model, AppInitializerView view, IAppInitializerContext context) : base(model, view)
        {
            _context = context;
        }

        public override void Initialize()
        {
            SendAppInitializedEventAfterDelay().Forget();
        }

        //TODOBY think about execution orders
        private async UniTask SendAppInitializedEventAfterDelay()
        {
            await UniTask.Delay(2000);
            UnityEngine.Debug.Log("Published AppInitialized Event");
            _context.EventBusGlobal.Publish(new AppInitializedEvent() { Time = UnityEngine.Time.time });
        }
    }
}
