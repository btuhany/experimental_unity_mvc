using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using R3;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TimeCounter.Entities.SceneChanger
{
    public interface ISceneChangerViewModel : IView
    {
        ReactiveCommand OnButtonClickedCommand { get; }
    }
    public class SceneChangerViewModel : BaseViewMonoBehaviour, ISceneChangerViewModel
    {
        [SerializeField] private Button _button;
        public ReactiveCommand OnButtonClickedCommand { get; private set; }
        public override Type ContractTypeToBind => typeof(ISceneChangerViewModel);
        private IDisposable _disposable;
        private void Awake()
        {
            OnButtonClickedCommand = new ReactiveCommand();
            _disposable = _button.onClick.AsObservable().Subscribe(_ => OnButtonClickedCommand.Execute(Unit.Default));

            //GameObject already is a child of DDOL Object : UIMainCanvasSSO
            //transform.SetParent(null);
            //DontDestroyOnLoad(this);
        }
        private void OnDestroy()
        {
            Dispose();
        }
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
