using Batuhan.MVC.Base;
using Batuhan.MVC.Core;

namespace ExperimentalMVC.App.Entities
{
    public class MainSSOCanvasController : BaseControllerWithViewOnly<IMainSSOCanvasView>, IAppLifeCycleManaged
    {
        public MainSSOCanvasController(IMainSSOCanvasView view) : base(view)
        {

        }

        public AppLifeCycleManagedDelegate RemoveFromAppLifeCycleAction {  get; set; }

        public void Initialize()
        {
            _view.TestLog("controller init");
        }
    }
}
