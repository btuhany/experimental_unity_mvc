using System;
using Batuhan.MVC.UnityComponents.Base;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ExperimentalMVC.SnakeExample.Entities.MainCanvasSSO
{
    public class SnakeCanvasSSOView : BaseViewMonoBehaviour
    {
        public override Type ContractTypeToBind => typeof(SnakeCanvasSSOView);

        [SerializeField] private TextMeshProUGUI _text;

        public override void Dispose()
        {
            base.Dispose();
        }

        public void ShowPressAny()
        {
            _text.gameObject.SetActive(true);
            _text.text = "Press Any...";
        }

        public void ShowStartAsync()
        {
            ShowStartAsyncInternal().Forget();
        }

        private async UniTaskVoid ShowStartAsyncInternal()
        {
            _text.gameObject.SetActive(true);
            _text.text = "Start!";
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _text.gameObject.SetActive(false);
        }
        public void ShowGameOver()
        {
            _text.gameObject.SetActive(true);
            _text.text = "Game Over";
        }
    }
}