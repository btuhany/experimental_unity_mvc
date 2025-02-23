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
        ReactiveCommand OnNextSceneButtonClicked { get; }
        ReactiveCommand OnPrevSceneButtonClicked { get; }
        void SetActiveNextSceneButtonGameObject(bool active);
        void SetActivePrevSceneButtonGameObject(bool active);
    }
    public class SceneChangerViewModel : BaseSingletonViewMonoBehaviour<SceneChangerViewModel>, ISceneChangerViewModel
    {
        [SerializeField] private Button _nextSceneButton;
        [SerializeField] private Button _previousSceneButton;
        public ReactiveCommand OnNextSceneButtonClicked { get; private set; }
        public override Type ContractTypeToBind => typeof(ISceneChangerViewModel);

        public ReactiveCommand OnPrevSceneButtonClicked { get; private set; }

        private IDisposable _disposable;
        private void Awake()
        {
            if (!TrySetSingletonAsDDOL(true))
            {
                return;
            }

            OnNextSceneButtonClicked = new ReactiveCommand();
            OnPrevSceneButtonClicked = new ReactiveCommand();

            var disposableBuilder = Disposable.CreateBuilder();
            _nextSceneButton.onClick.AsObservable().Subscribe(_ => OnNextSceneButtonClicked.Execute(Unit.Default)).AddTo(ref disposableBuilder);
            _previousSceneButton.onClick.AsObservable().Subscribe(_ => OnPrevSceneButtonClicked.Execute(Unit.Default)).AddTo(ref disposableBuilder);

            _disposable = disposableBuilder.Build();
        }
        public override void Dispose()
        {
            _disposable?.Dispose();
            base.Dispose();
        }
        public void SetActiveNextSceneButtonGameObject(bool active)
        {
            _nextSceneButton.gameObject.SetActive(active);
        }
        public void SetActivePrevSceneButtonGameObject(bool active)
        {
            _previousSceneButton.gameObject.SetActive(active);
        }
    }
}
