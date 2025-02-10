using Batuhan.Core.MVC;
using Batuhan.Core.MVC.Unity;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterView : BaseViewMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        private ICounterContext _counterContext;
        public override IContext Context => _counterContext;

        public void Initialize()
        {
            //TODO MVC Entity Initialized and Ready to Use Event
            _counterText.text = "-";
        }
        public void OnCountChanged(int count)
        {
            _counterText.text = count.ToString();
        }
    }
}
