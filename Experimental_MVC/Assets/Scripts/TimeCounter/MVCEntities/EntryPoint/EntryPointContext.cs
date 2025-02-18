using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.EntryPoint
{
    internal interface IEntryPointContext : IContext
    {
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        public IDebugHelper Debug { get; }

    }
    internal class EntryPointContext : IEntryPointContext
    {
        [Inject]
        public IEventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        [Inject]
        public IDebugHelper Debug { get; }

    }
}
