using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using Cysharp.Threading.Tasks;
using System;

namespace ExperimentalMVC.App.Entities
{
    public interface IMainSSOCanvasView : IView
    {
    }
    public class MainSSOCanvasView : BaseViewMonoBehaviour, IMainSSOCanvasView
    {
        public override Type ContractTypeToBind => typeof(IMainSSOCanvasView);

        private void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }
      
    }
}
