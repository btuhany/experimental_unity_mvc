using Batuhan.CommandManager;

namespace TimeCounter.Commands
{
    public struct TriggerAnimatorHashCommand : ICommand
    {
        private int _triggerHash;
        private UnityEngine.Animator _animator;
        public TriggerAnimatorHashCommand(int triggerHash, UnityEngine.Animator animator)
        {
            _triggerHash = triggerHash;
            _animator = animator;
        }

        public void OnBindingExecute()
        {
        }

        public void OnBindingUndo()
        {
        }

        public void OnExecute()
        {
            _animator.SetTrigger(_triggerHash);
        }
        public void OnUndo()
        {
        }
    }
}
