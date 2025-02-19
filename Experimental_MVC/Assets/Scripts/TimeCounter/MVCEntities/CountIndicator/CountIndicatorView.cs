using Assets.Scripts.TimeCounter.Commands;
using Batuhan.CommandManager;
using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Base;
using System;
using TimeCounter.Commands;
using TimeCounter.Events.ModelEvents;
using TMPro;
using UnityEngine;
namespace TimeCounter.Entities.CountIndicator
{
    public interface ICountIndicatorView : IViewContextual<ICountIndicatorContext>
    {

    }
    internal class CountIndicatorView : BaseViewMonoBehaviour, ICountIndicatorView
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private ICountIndicatorContext _context;
        public ICountIndicatorContext Context => _context;

        public override Type ContractTypeToBind => typeof(IViewContextual<ICountIndicatorContext>);

        public void Setup(ICountIndicatorContext context)
        {
            _context = context;
            _text.SetText("-");
            _context.EventBusModel.Subscribe<CountIndicatorDataUpdatedEvent>(OnIndicatorDataUpdated);
            _context.CommandManager.AddListener(new CommandBinding<SetParentCommand>(OnSetParentCommand));
            _context.CommandManager.AddListener(new CommandBinding<DestroyGameObjectCommand>(OnDestroyGameObjectCommand));
        }
        private void OnDestroyGameObjectCommand(DestroyGameObjectCommand commandData)
        {
            Destroy(gameObject);
        }
        public void Dispose()
        {
            _context.EventBusModel.Unsubscribe<CountIndicatorDataUpdatedEvent>(OnIndicatorDataUpdated);
            _context.CommandManager.RemoveListenerFromExecuteCallback<SetParentCommand>(OnSetParentCommand);
            _context.CommandManager.RemoveListenerFromExecuteCallback<DestroyGameObjectCommand>(OnDestroyGameObjectCommand);
        }
        private void OnSetParentCommand(SetParentCommand commandData)
        {
            _transform.SetParent(commandData.Parent);
        }
        private void OnIndicatorDataUpdated(CountIndicatorDataUpdatedEvent eventData)
        {
            var commonData = eventData.Data;
            _text.text = commonData.Index.ToString();
            _spriteRenderer.color = commonData.Color;
            _transform.position = commonData.Position;
        }
    }
}
