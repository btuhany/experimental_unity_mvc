using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using UnityEngine;
namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal class CountIndicatorView : BaseViewMonoBehaviour
    {
        private ICountIndicatorContext _circleContext;
        public override IContext Context => _circleContext;

        //TODOBY Think about integrating it with zenject
        public void SetContext(ICountIndicatorContext circleContext)
        {
            _circleContext = circleContext;
        }
        public void Initialize()
        {
            _circleContext.ChangeColorEvent += OnChangeColorEvent;
        }

        private void OnChangeColorEvent()
        {
            Debug.Log("On Event Invoked!");
            var randomColor = UnityEngine.Random.ColorHSV();
            randomColor.a = 1.0f;
            GetComponent<SpriteRenderer>().color = randomColor;
        }
        private void OnDestroy()
        {
            _circleContext.ChangeColorEvent -= OnChangeColorEvent;
        }
    }
}
