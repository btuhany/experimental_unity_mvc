using Batuhan.Core.MVC;
using Batuhan.Core.MVC.Unity;
using System;
using UnityEngine;
using Zenject;
namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal class CircleView : BaseViewMonoBehaviour
    {
        private ICircleContext _circleContext;
        public override IContext Context => _circleContext;

        //TODOBY Think about integrating it with zenject
        public void SetContext(ICircleContext circleContext)
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
