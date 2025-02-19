using Batuhan.CommandManager;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using System;
using TimeCounter.Commands;
using TMPro;
using UnityEngine;

namespace TimeCounter.Entities.CounterText
{
    public interface ICounterTextView : IViewContextual<ICounterTextContext>
    {
        void OnCounterTextUpdated(string str);
        //Animator Animator { get; }
    }
    internal class CounterTextView : BaseViewMonoBehaviour, ICounterTextView
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Animator _animator;

        //public Animator Animator => _animator;

        private ICounterTextContext _context;
        public ICounterTextContext Context => _context;

        public override Type ContractTypeToBind => typeof(ICounterTextView);

        public void Setup(ICounterTextContext context)
        {
            _context = context;
            _textMesh.SetText(string.Empty);
            RegisterCommandListeners();
        }

        public void OnCounterTextUpdated(string str)
        {
            _textMesh.SetText(str);
        }
        public void Dispose()
        {
            UnregisterCommandListeners();
        }
        private void RegisterCommandListeners()
        {
            _context.CommandManager.AddListener(new CommandBinding<AnimateCounterTextCommand>(OnExecuteUpdateCounterText, null));
        }
        private void UnregisterCommandListeners()
        {
            _context.CommandManager.RemoveListenerFromExecuteCallback<AnimateCounterTextCommand>(OnExecuteUpdateCounterText);
        }

        private void OnExecuteUpdateCounterText(AnimateCounterTextCommand commandData)
        {
            if (commandData.ParameterType == AnimatorControllerParameterType.Trigger)
                _animator.SetTrigger(commandData.ParameterHash);
        }
    }
}
