using Batuhan.RuntimeClonableScriptableObjects;
using System;
using UnityEngine;

namespace TimeCounter.Data
{
    [CreateAssetMenu(fileName = "CounterTextDataSO", menuName = "Scriptable Objects/Batuhan/Model_DataSO/TimeTickerModelDataSO")]
    public class TimeTickerModelDataSO : RuntimeClonableScriptableObject
    {
        [SerializeField] private float _tickSpeed = 1.0f;
        [SerializeField] private int _maxTickCount = 100;
        [SerializeField] private int _minTickCount = 0;
        [NonSerialized] private int _tickCount = 0;
        public float TickSpeed { get => _tickSpeed; set => _tickSpeed = value; }
        public int TickCount { get => _tickCount; set => _tickCount = value; }
        public int MaxTickCount { get => _maxTickCount; }
        public int MinTickCount { get => _minTickCount; }
        public bool MaxTickCountReached => _maxTickCount == _tickCount;
        public bool MinTickCountReached => _minTickCount == _tickCount;
    }
}
