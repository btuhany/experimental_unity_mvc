using Batuhan.RuntimeClonableScriptableObjects;
using UnityEngine;

namespace TimeCounter.Data
{
    [CreateAssetMenu(fileName = "CounterTextDataSO", menuName = "Scriptable Objects/Batuhan/ModelData/CounterText")]
    internal class CounterTextModelDataSO : BaseRuntimeClonableScriptableObject
    {
        [SerializeField] private float _countSpeed = 1.0f;
        [SerializeField] private int _counterValue = 0;
        public float CountSpeed { get => _countSpeed; set => _countSpeed = value; }
        public int CounterValue { get => _counterValue; set => _counterValue = value; }
    }
}
