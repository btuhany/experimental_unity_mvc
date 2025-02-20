namespace Batuhan.CommandManager.TestCommands
{
    public class OtherCommand : ICommand
    {
        public void OnBindingExecute()
        {
        }

        public void OnBindingUndo()
        {
        }

        public void OnExecute() { }
        public void OnUndo() { }
    }
    public class DummyCommand : ICommand
    {
        public bool OnExecuteCalled { get; private set; }
        public bool OnUndoCalled { get; private set; }

        public void OnBindingExecute()
        {
        }

        public void OnBindingUndo()
        {
        }

        public void OnExecute() => OnExecuteCalled = true;
        public void OnUndo() => OnUndoCalled = true;
    }
    public class TestCommand : ICommand
    {
        public bool ExecuteCalled { get; private set; }
        public bool UndoCalled { get; private set; }

        public void OnBindingExecute()
        {
        }

        public void OnBindingUndo()
        {
        }

        public void OnExecute()
        {
            ExecuteCalled = true;
        }

        public void OnUndo()
        {
            UndoCalled = true;
        }
    }
}
