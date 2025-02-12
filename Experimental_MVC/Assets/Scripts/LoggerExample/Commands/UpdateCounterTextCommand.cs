using Batuhan.CommandManager;

namespace Assets.Scripts.LoggerExample.Commands
{
    public struct UpdateCounterTextCommand : ICommand
    {
        public int CounterValue { get; }
        public UpdateCounterTextCommand(int counterValue)
        {
            CounterValue = counterValue;
        }
        public void OnExecute()
        {
        }
        public void OnUndo()
        {
        }
    }
}
