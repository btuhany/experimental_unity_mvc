using Batuhan.RuntimeClonableScriptableObjects;
using System;
using UnityEngine;

namespace TimeCounter.Data
{
    [CreateAssetMenu(fileName = "CounterTextDataSO", menuName = "Scriptable Objects/Batuhan/Model_DataSO/TimeTickerModelDataSO")]
    public class TimeTickerModelDataSO : RuntimeClonableScriptableObject
    {
        [SerializeField] private float _tickSpeed = 1.0f;
        [NonSerialized] private int _tickCount = 0;
        public float TickSpeed { get => _tickSpeed; set => _tickSpeed = value; }
        public int TickCount { get => _tickCount; set => _tickCount = value; }
    }
}
