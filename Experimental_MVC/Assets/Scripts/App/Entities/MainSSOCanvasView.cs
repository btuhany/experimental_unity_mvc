using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using System;
using UnityEngine;

namespace ExperimentalMVC.App.Entities
{
    public interface IMainSSOCanvasView : IView
    {
        void TestLog(string message);
    }
    public class MainSSOCanvasView : BaseViewMonoBehaviour, IMainSSOCanvasView
    {
        public override Type ContractTypeToBind => typeof(IMainSSOCanvasView);
        private int _testRandomInt = 0;

        public void Dispose()
        {
        }

        public void TestLog(string message)
        {
            Debug.Log("MainSSOCanvasView " + message);
        }

        private void Awake()
        {
            _testRandomInt = UnityEngine.Random.Range(1, 100);
            Debug.Log("MainSSOCanvasView randomInt set to:" + _testRandomInt.ToString());

            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }
    }
}
