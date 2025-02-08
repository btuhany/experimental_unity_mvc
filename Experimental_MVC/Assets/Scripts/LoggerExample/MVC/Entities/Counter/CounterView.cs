using Assets.Scripts.Batuhan.Core.MVC.Base;
using UnityEngine;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterView
        : BaseView
    {
        private void OnCountChanged(int count)
        {
            Debug.Log($"Count changed: {count}");
        }
    }
}
