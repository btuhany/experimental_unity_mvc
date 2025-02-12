namespace Batuhan.CommandManager
{
    public interface ICommand
    {
        void OnExecute();
        void OnUndo();
    }
}
