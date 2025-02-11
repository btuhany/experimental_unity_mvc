using Assets.Scripts.LoggerExample.Commands;
using Batuhan.CommandManager;
using Batuhan.Core.MVC;
using Batuhan.Core.MVC.Unity;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Counter
{
    internal class CounterView : BaseViewMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        private ICounterContext _counterContext;
        private CommandBinding<UpdateCounterTextCommand> _updateCounterTextCommandBinding;
        private CommandBinding<UpdateCounterTextCommand> _updateCounterTextCommandBinding2;

        public override IContext Context => _counterContext;

        public void Initialize(ICounterContext context)
        {
            //TODO MVC Entity Initialized and Ready to Use Event
            _counterContext = context;
            _counterText.text = "-";
            _updateCounterTextCommandBinding = new CommandBinding<UpdateCounterTextCommand>(OnUpdateCounterTextExecuteCommand);
            _counterContext.CommandManager.AddListener(_updateCounterTextCommandBinding);

            _updateCounterTextCommandBinding2 = new CommandBinding<UpdateCounterTextCommand>(OnUpdateCounterTextExecuteCommand2,null,5);
            _counterContext.CommandManager.AddListener(_updateCounterTextCommandBinding2);
        }
        public void RemoveTextCommandListener()
        {
            _counterContext.CommandManager.RemoveListenerFromExecuteCallback<UpdateCounterTextCommand>(OnUpdateCounterTextExecuteCommand);
            _counterContext.CommandManager.RemoveListener(_updateCounterTextCommandBinding2);
        }
        private void OnDestroy()
        {
            RemoveTextCommandListener();
        }
        public void OnUpdateCounterTextExecuteCommand(UpdateCounterTextCommand commandData)
        {
            _counterText.text = commandData.CounterValue.ToString();
            UnityEngine.Debug.Log("Command 1 Executed");
        }
        public void OnUpdateCounterTextExecuteCommand2(UpdateCounterTextCommand commandData)
        {
            UnityEngine.Debug.Log("Command 2 Executed");
        }
    }
}
