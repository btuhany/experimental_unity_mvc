using Assets.Scripts.Batuhan.Core.MVC.Base;
using UnityEngine;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Logger
{
    internal class LoggerView : BaseView
    {
        private void OnCountChanged(int count)
        {
            Debug.Log($"Count changed: {count}");
        }
    }
}
