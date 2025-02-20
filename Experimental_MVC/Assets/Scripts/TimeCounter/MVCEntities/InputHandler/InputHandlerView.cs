using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimeCounter.Entities.InputHandler
{
    public interface IInputHandlerView : IView
    {
        ReactiveCommand SpeedUpCommand { get; }
        ReactiveCommand SpeedDownCommand { get; }
        ReactiveCommand SetMaxCommand { get; }
        ReactiveCommand SetMinCommand { get; }
        ReactiveCommand SetCountCommand { get; }

        ReadOnlyReactiveProperty<string> SetMaxCountField { get; }
        ReadOnlyReactiveProperty<string> SetMinCountField { get; }
        ReadOnlyReactiveProperty<string> SetCountField { get; }
    }
    public class InputHandlerView : BaseViewMonoBehaviour, IInputHandlerView
    {
        [SerializeField] private Button _speedUpButton;
        [SerializeField] private Button _speedDownButton;

        [SerializeField] private TMP_InputField _setMaxCountField;
        [SerializeField] private Button _setMaxCountButton;

        [SerializeField] private TMP_InputField _setMinCountField;
        [SerializeField] private Button _setMinCountButton;

        [SerializeField] private TMP_InputField _setCountField;
        [SerializeField] private Button _setCountButton;

        private DisposableBag _disposables;
        public override Type ContractTypeToBind => typeof(IInputHandlerView);

        public ReactiveCommand SpeedUpCommand { get; private set; }

        public ReactiveCommand SpeedDownCommand { get; private set; }

        public ReactiveCommand SetMaxCommand { get; private set; }

        public ReactiveCommand SetMinCommand { get; private set; }

        public ReactiveCommand SetCountCommand { get; private set; }

        public ReadOnlyReactiveProperty<string> SetMaxCountField { get; private set; }

        public ReadOnlyReactiveProperty<string> SetMinCountField { get; private set; }

        public ReadOnlyReactiveProperty<string> SetCountField { get; private set; }

        private void Awake()
        {
            SpeedUpCommand = new ReactiveCommand();
            SpeedDownCommand = new ReactiveCommand();
            SetMaxCommand = new ReactiveCommand();
            SetMinCommand = new ReactiveCommand();
            SetCountCommand = new ReactiveCommand();

            _speedUpButton.OnClickAsObservable().Subscribe(_ => SpeedUpCommand.Execute(Unit.Default)).AddTo(ref _disposables);
            _speedDownButton.OnClickAsObservable().Subscribe(_ => SpeedDownCommand.Execute(Unit.Default)).AddTo(ref _disposables);
            _setMaxCountButton.OnClickAsObservable().Subscribe(_ => SetMaxCommand.Execute(Unit.Default)).AddTo(ref _disposables);
            _setMinCountButton.OnClickAsObservable().Subscribe(_ => SetMinCommand.Execute(Unit.Default)).AddTo(ref _disposables);
            _setCountButton.OnClickAsObservable().Subscribe(_ => SetCountCommand.Execute(Unit.Default)).AddTo(ref _disposables);

            SetMaxCountField = _setMaxCountField.onValueChanged.AsObservable().ToReadOnlyReactiveProperty().AddTo(ref _disposables);
            SetMinCountField = _setMinCountField.onValueChanged.AsObservable().ToReadOnlyReactiveProperty().AddTo(ref _disposables);
            SetCountField = _setCountField.onValueChanged.AsObservable().ToReadOnlyReactiveProperty().AddTo(ref _disposables);
        }
        private void DebugLog()
        {

        }
        private void OnDestroy()
        {
            Dispose();
        }
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
