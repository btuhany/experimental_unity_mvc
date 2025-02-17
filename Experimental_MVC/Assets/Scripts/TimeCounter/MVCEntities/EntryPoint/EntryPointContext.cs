using Assets.Scripts.TimeCounter.Helper;
using Batuhan.CommandManager;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using Zenject;

namespace TimeCounter.Entities.EntryPoint
{
    internal interface IEntryPointContext : IContext
    {
        public EventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        public DebugHelper Debug { get; }

    }
    internal class EntryPointContext : IEntryPointContext
    {
        [Inject]
        public EventBus<Events.GlobalEvents.Global> EventBusGlobal { get; }
        [Inject]
        public DebugHelper Debug { get; }

    }
}
