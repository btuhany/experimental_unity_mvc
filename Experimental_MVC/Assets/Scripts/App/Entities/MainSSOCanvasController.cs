using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;

namespace ExperimentalMVC.App.Entities
{
    public class MainSSOCanvasController : BaseControllerWithViewOnly<IMainSSOCanvasView>, ISceneLifeCycleManaged
    {
        public int RandomInt;
        public MainSSOCanvasController(IMainSSOCanvasView view) : base(view)
        {

        }


        public void Initialize()
        {
            
        }

        public void OnAwakeCallback()
        {
            RandomInt = UnityEngine.Random.Range(1, 100);
            _view.TestLog("controller init with random int: " + RandomInt);
            _view.Controller = this;
            //DestoryAfterSomeTime().Forget();
        }

        public void OnDestroyCallback()
        {
            Dispose();
        }

        //private async UniTask DestoryAfterSomeTime()
        //{
        //    await UniTask.Delay(2000);
        //    UnityEngine.Debug.Log("DestoryAfterSomeTime");
        //    RemoveFromAppLifeCycleAction?.Invoke(this);
        //}
    }
}
