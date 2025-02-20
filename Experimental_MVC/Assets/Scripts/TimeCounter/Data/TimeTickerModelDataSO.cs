using Batuhan.CustomEditor;
using Batuhan.RuntimeClonableScriptableObjects;
using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;

namespace TimeCounter.Data
{
    [CreateAssetMenu(fileName = "CounterTextDataSO", menuName = "Scriptable Objects/Batuhan/Model_DataSO/TimeTickerModelDataSO")]
    public class TimeTickerModelDataSO : RuntimeClonableScriptableObject, IDisposable
    {
        [SerializeField] private int _maxTickCount = 100;
        [SerializeField] private int _minTickCount = 0;
        [SerializeField] private int _initialTickCount= 0;
        [SerializeField] private float _initialTickSpeed= 1.0f;
        [NonSerialized] private float _tickSpeed = 1.0f;
        [NonSerialized] private int _tickCount = 0;

        public int MaxTickCount { get => _maxTickCount; }
        public int MinTickCount { get => _minTickCount; }
        public bool MaxTickCountReached => _maxTickCount == _tickCount;
        public bool MinTickCountReached => _minTickCount == _tickCount;
        public ReactiveProperty<int> TickCount { get; private set; }

        public ReactiveProperty<float> TickSpeed { get; private set; }

        private IDisposable _reactivePropertyDisposables;
        public void Initialize()
        {
            _tickSpeed = 1.0f;
            _tickCount = _initialTickCount;

            var disposableBuilder = Disposable.CreateBuilder();
            TickCount = new ReactiveProperty<int>(_tickCount).AddTo(ref disposableBuilder);
            TickSpeed = new ReactiveProperty<float>(_tickSpeed).AddTo(ref disposableBuilder);

            TickCount.Subscribe(newValue => _tickCount = newValue).AddTo(ref disposableBuilder);
            TickSpeed.Subscribe(newValue => _tickSpeed = newValue).AddTo(ref disposableBuilder);

            _reactivePropertyDisposables = disposableBuilder.Build();
        }
        public void Dispose()
        {
            _reactivePropertyDisposables?.Dispose();
        }

#if UNITY_EDITOR

        [Button("UPDATE TICK COUNT RUNTIME DATA")]
        private void EDITOR_UpdateRuntimeCountDataToInitials()
        {
            if (TickSpeed != null)
                TickCount.Value = _initialTickCount;
        }
        [Button("UPDATE TICK SPEED RUNTIME DATA")]
        private void EDITOR_UpdateRuntimeSpeedDataToInitials()
        {
            if (TickSpeed != null)
                TickSpeed.Value = _initialTickSpeed;
        }
    }
#endif
}
