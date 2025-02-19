using Batuhan.CommandManager;
using UnityEngine;

namespace TimeCounter.Commands
{
    public struct AnimateCounterTextCommand : IAnimatorSetParameterCommand
    {
        public AnimatorControllerParameterType ParameterType { get; set; }
        public int ParameterHash { get; set; }
        public AnimateCounterTextCommand(int clipHash, AnimatorControllerParameterType parameterType)
        {
            ParameterHash = clipHash;
            ParameterType = parameterType;
        }

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
    public interface IAnimatorSetParameterCommand : ICommand
    {
        public AnimatorControllerParameterType ParameterType { get; set; }
        public int ParameterHash { get; set; }
    }
}
