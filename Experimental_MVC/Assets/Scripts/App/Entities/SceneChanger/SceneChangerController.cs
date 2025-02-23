using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using Cysharp.Threading.Tasks;
using ExperimentalMVC.App.Entities;
using R3;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TimeCounter.Entities.SceneChanger
{
    public class SceneChangerController : BaseControllerWithViewOnly<ISceneChangerViewModel>, IAppLifeCycleManaged
    {
        private IDisposable _viewModelSubDisposable;
        private AppLifeCycleManagedDelegate _destroyDelegate;
        public AppLifeCycleManagedDelegate RemoveFromAppLifeCycleAction { get => _destroyDelegate; set => _destroyDelegate = value; }
        public ReferenceAllocationMode AllocationMode => ReferenceAllocationMode.Singleton;

        public Type AllocationRegistrationType => typeof(SceneChangerController);

        public SceneChangerController(ISceneChangerViewModel view) : base(view)
        {
        }

        public void Initialize()
        {
            var disposableBuilder = Disposable.CreateBuilder();
            _viewModel.OnNextSceneButtonClicked.Subscribe(_ => OnNextButtonClicked()).AddTo(ref disposableBuilder);
            _viewModel.OnPrevSceneButtonClicked.Subscribe(_ => OnPrevButtonClicked()).AddTo(ref disposableBuilder);
            _viewModelSubDisposable = disposableBuilder.Build();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene sceneData, LoadSceneMode loadSceneMode)
        {
            int buildIndex = sceneData.buildIndex;
            if (buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                //Handle last scene operations
                _viewModel.SetActiveNextSceneButtonGameObject(false);
                _viewModel.SetActivePrevSceneButtonGameObject(true);
            }
            else if (buildIndex == 0)
            {
                //Handle first scene operations
                _viewModel.SetActiveNextSceneButtonGameObject(true);
                _viewModel.SetActivePrevSceneButtonGameObject(false);
            }
            else
            {
                _viewModel.SetActiveNextSceneButtonGameObject(true);
                _viewModel.SetActivePrevSceneButtonGameObject(true);
            }
        }
        private void OnPrevButtonClicked()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            ChangeScene(currentSceneIndex - 1);
        }

        public override void Dispose()
        {
            UnityEngine.Debug.Log("SceneChanger controller disposed!");
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _viewModelSubDisposable?.Dispose();
            base.Dispose();
        }
        private void OnNextButtonClicked()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            ChangeScene(currentSceneIndex + 1);
        }

        public void ChangeScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        public async UniTaskVoid LoadSceneAsyncByIndex(int sceneIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress < 0.9f)
                {
                    Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
                }
                Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
                await UniTask.Yield();
            }
        }
    }
}
