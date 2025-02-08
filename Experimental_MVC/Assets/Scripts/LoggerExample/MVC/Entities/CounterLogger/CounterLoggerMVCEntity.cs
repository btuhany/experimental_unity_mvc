using Batuhan.Core.MVC.Base;
using UnityEngine;
namespace Assets.Scripts.LoggerExample.MVC.Entities.CounterLogger
{
    internal class CounterLoggerMVCEntity : BaseEntity<CounterLoggerModel, CounterLoggerView, CounterLoggerController>
    {
        public override void Initialize()
        {
            Debug.Log("CounterLoggerMVCEntity is initialized");
        }
    }
}
