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
    }
    internal class CounterTextView : BaseViewComponent, ICounterTextView
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        private ICounterTextContext _context;

        public ICounterTextContext Context => _context;

        public override Type ContractTypeToBind => typeof(IViewContextual<ICounterTextContext>);

        public void Setup(ICounterTextContext context)
        {
            _context = context;
            RegisterCommandListeners();
            _textMesh.SetText("-");
        }

        public void Dispose()
        {
            UnregisterCommandListeners();
        }
        private void RegisterCommandListeners()
        {
            _context.CommandManager.AddListener(new CommandBinding<UpdateCounterTextCommand>(OnExecuteUpdateCounterText, null));
        }
        private void UnregisterCommandListeners()
        {
            _context.CommandManager.RemoveListenerFromExecuteCallback<UpdateCounterTextCommand>(OnExecuteUpdateCounterText);
        }

        private void OnExecuteUpdateCounterText(UpdateCounterTextCommand commandData)
        {
            var counterValue = commandData.Text;
            _textMesh.SetText(counterValue.ToString());
        }
    }
}
