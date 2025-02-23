using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using System;

namespace ExperimentalMVC.App.Entities
{
    public class MainSSOCanvasController : BaseControllerWithViewOnly<IMainSSOCanvasView>, IAppLifeCycleManaged
    {
        public MainSSOCanvasController(IMainSSOCanvasView view) : base(view)
        {

        }

        public AppLifeCycleManagedDelegate RemoveFromAppLifeCycleAction { get; set; }

        public ReferenceAllocationMode AllocationMode => ReferenceAllocationMode.Singleton;

        public Type AllocationRegistrationType => typeof(MainSSOCanvasController);

        public void Initialize()
        {
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
