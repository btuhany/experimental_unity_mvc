using Batuhan.CommandManager;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using TimeCounter.Commands;
using TMPro;
using UnityEngine;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextView : BaseViewMonoBehaviour<ICounterTextContext>
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        public override void Setup(ICounterTextContext context)
        {
            base.Setup(context);
            RegisterCommandListeners();
            _textMesh.SetText("-");
        }

        public override void Dispose()
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
            var counterValue = commandData.CounterValue;
            _textMesh.SetText(counterValue.ToString());
        }
    }
}
