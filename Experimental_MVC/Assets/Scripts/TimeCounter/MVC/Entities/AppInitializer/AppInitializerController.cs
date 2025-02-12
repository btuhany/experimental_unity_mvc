using Batuhan.MVC.Base;
using Cysharp.Threading.Tasks;
using TimeCounter.Events.Global;
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
            SendAppInitializedEventAfterDelay().Forget();
        }

        //TODOBY think about execution orders
        private async UniTask SendAppInitializedEventAfterDelay()
        {
            await UniTask.Delay((int)_model.InitializationDelay);
            UnityEngine.Debug.Log("Published AppInitialized Event");
            _context.EventBusGlobal.Publish(new AppInitializedEvent() { Time = UnityEngine.Time.time });
        }
    }
}
