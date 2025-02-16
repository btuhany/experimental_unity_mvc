using Batuhan.CommandManager;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using TimeCounter.Commands;
using TMPro;
using UnityEngine;

namespace TimeCounter.Entities.CounterText
{
    internal class CounterTextView : BaseViewMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        private ICounterTextContext _context;
        public override IContext Context => _context;

        public void Setup(ICounterTextContext context)
        {
            _context = context;
            _textMesh.SetText("-");
            
            RegisterCommandListeners();
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

        private void OnExecuteUpdateCounterText(UpdateCounterTextCommand command)
        {

        }
    }
}
