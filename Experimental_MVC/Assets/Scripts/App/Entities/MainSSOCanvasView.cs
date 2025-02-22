using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using Cysharp.Threading.Tasks;
using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExperimentalMVC.App.Entities
{
    public interface IMainSSOCanvasView : IView
    {
        void TestLog(string message);
        MainSSOCanvasController Controller { get; set; }
    }
    public class MainSSOCanvasView : BaseViewMonoBehaviour, IMainSSOCanvasView
    {
        public override Type ContractTypeToBind => typeof(IMainSSOCanvasView);
        private int _testRandomInt = 0;
        public MainSSOCanvasController Controller { get; set; }
        public override void Dispose()
        {
            Debug.Log("MainSSOCanvasView disposed");
        }

        public void TestLog(string message)
        {
            Debug.Log("MainSSOCanvasView " + message);
        }

        private void Awake()
        {
            _testRandomInt = UnityEngine.Random.Range(1, 100);
            Debug.Log("MainSSOCanvasView randomInt set to:" + _testRandomInt.ToString());

            SceneManager.sceneLoaded += HandleOnSceneLoaded;

            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }

        private void HandleOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            TestLogAfterSceneLoaded().Forget();
        }
        private async UniTask TestLogAfterSceneLoaded()
        {
            await UniTask.Delay(1000);
            if (Controller != null)
            {
                Debug.Log("MainSSOCanvasView controller is not null with random int: " + Controller.RandomInt);
            }
            else
            {
                Debug.Log("MainSSOCanvasView controller is null");
            }
        }
    }
}
