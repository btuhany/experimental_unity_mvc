using Batuhan.MVC.Base;
using Batuhan.MVC.Core;
using Cysharp.Threading.Tasks;
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

        public SceneChangerController(ISceneChangerViewModel view) : base(view)
        {
        }

        public void Initialize()
        {
            _viewModelSubDisposable = _view.OnButtonClickedCommand.Subscribe(_ => OnButtonClicked());
        }
        public override void Dispose()
        {
            base.Dispose();
            _viewModelSubDisposable?.Dispose();
        }
        private void OnButtonClicked()
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
