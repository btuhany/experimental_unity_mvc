namespace Batuhan.CommandManager
{
    public interface ICommand
    {
        //One time before listener callbacks
        void OnExecute();
        //One time before listener callbacks
        void OnUndo();
        //Called after every listener callback
        void OnBindingExecute();
        //Called after every listener callback
        void OnBindingUndo();
    }
}
