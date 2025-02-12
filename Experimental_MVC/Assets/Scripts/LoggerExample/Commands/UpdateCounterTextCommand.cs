using Batuhan.CommandManager;

namespace TimeCounter.Commands
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
            UnityEngine.Debug.Log($"Counter value updated to {CounterValue}");
        }
        public void OnUndo()
        {
        }
    }
}
