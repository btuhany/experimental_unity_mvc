using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Core;
using System.Collections.Generic;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public class ProjectReferenceManager : IProjectReferenceManager
    {
        private List<IProjectLifeCycleManaged> _projectLifeCycleReferences = new List<IProjectLifeCycleManaged>();
        
        public void AddToProjectLifeCycleReferences(IProjectLifeCycleManaged projectLifeCycle)
        {
            _projectLifeCycleReferences.Add(projectLifeCycle);
        }
    }
}
