using Batuhan.MVC.UnityComponents.Base;
using TMPro;
using UnityEngine;
namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorView : BaseViewMonoBehaviour<ICountIndicatorContext>
    {
        [SerializeField] private TextMeshProUGUI _text;
        public override void Setup(ICountIndicatorContext context)
        {
            base.Setup(context);
            transform.position = Random.insideUnitCircle * 4;
            _text.SetText("-");
        }
        public override void Dispose()
        {
        }
    }
}
