using Batuhan.CommandManager;

namespace Assets.Scripts.TimeCounter.Commands
{
    public struct SetParentCommand : ICommand
    {
        public UnityEngine.Transform Parent;
        public void OnExecute()
        {
        }

        public void OnUndo()
        {
        }
    }
}
