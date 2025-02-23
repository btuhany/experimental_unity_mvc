using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using System;
using System.Diagnostics;

namespace ExperimentalMVC.App.Entities
{
    public interface IMainSSOCanvasView : IView
    {
    }
    public class MainSSOCanvasView : BaseSingletonViewMonoBehaviour<MainSSOCanvasView>, IMainSSOCanvasView
    {
        public override Type ContractTypeToBind => typeof(IMainSSOCanvasView);

        private void Awake()
        {
            if (!TrySetSingletonAsDDOL(true))
            {
                return;
            }
        }
      
    }
}
