using Batuhan.CommandManager;

namespace TimeCounter.Commands
{
    public struct UpdateCounterTextCommand : ICommand
    {
        public string Text { get; }
        public UpdateCounterTextCommand(string text)
        {
            Text = counterValue;
        }
        public void OnExecute()
        {
            
        }
        public void OnUndo()
        {
        }
    }
}
