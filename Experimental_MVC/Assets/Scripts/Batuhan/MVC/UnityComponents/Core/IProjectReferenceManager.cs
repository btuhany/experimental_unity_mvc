using Batuhan.MVC.Core;

namespace Batuhan.MVC.UnityComponents.Core
{
    public interface IProjectReferenceManager
    {
        void AddToProjectLifeCycleReferences(IProjectLifeCycleManaged projectLifeCycle);
    }
}
