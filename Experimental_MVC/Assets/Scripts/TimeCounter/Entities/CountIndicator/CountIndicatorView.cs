using Assets.Scripts.TimeCounter.Commands;
using Batuhan.CommandManager;
using Batuhan.MVC.UnityComponents.Base;
using TimeCounter.Events.ModelEvents;
using TMPro;
using UnityEngine;
namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorView : BaseViewMonoBehaviour<ICountIndicatorContext>
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public override void Setup(ICountIndicatorContext context)
        {
            base.Setup(context);
            _text.SetText("-");
            _context.EventBusModel.Subscribe<CountIndicatorDataUpdatedEvent>(OnIndicatorDataUpdated);
            _context.CommandManager.AddListener(new CommandBinding<SetParentCommand>(OnSetParentCommand));
        }
        public override void Dispose()
        {
            _context.EventBusModel.Unsubscribe<CountIndicatorDataUpdatedEvent>(OnIndicatorDataUpdated);
            _context.CommandManager.RemoveListenerFromExecuteCallback<SetParentCommand>(OnSetParentCommand);
        }
        private void OnSetParentCommand(SetParentCommand commandData)
        {
            _transform.SetParent(commandData.Parent);
        }
        private void OnIndicatorDataUpdated(CountIndicatorDataUpdatedEvent eventData)
        {
            var commonData = eventData.Data;
            _text.text = commonData.Indice.ToString();
            _spriteRenderer.color = commonData.Color;
            _transform.position = commonData.Position;
        }
    }
}
