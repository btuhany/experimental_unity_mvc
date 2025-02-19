using Batuhan.CommandManager;

namespace Assets.Scripts.TimeCounter.Commands
{
    public struct SetParentCommand : ICommand
    {
        public UnityEngine.Transform Parent;

        public void OnBindingExecute()
        {
        }

        public void OnBindingUndo()
        {
        }

        public void OnExecute()
        {
        }

        public void OnUndo()
        {
        }
    }
}
