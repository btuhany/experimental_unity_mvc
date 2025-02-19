using Batuhan.RuntimeClonableScriptableObjects;
using System;
using UnityEngine;

namespace TimeCounter.Data
{
    [CreateAssetMenu(fileName = "CounterTextDataSO", menuName = "Scriptable Objects/Batuhan/Model_DataSO/TimeTickerModelDataSO")]
    public class TimeTickerModelDataSO : RuntimeClonableScriptableObject
    {
        [SerializeField] private float _countSpeed = 1.0f;
        [NonSerialized] private int _counterValue = 0;
        public float CountSpeed { get => _countSpeed; set => _countSpeed = value; }
        public int CounterValue { get => _counterValue; set => _counterValue = value; }
    }
}
