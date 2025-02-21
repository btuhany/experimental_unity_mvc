using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Core;
using System.Collections.Generic;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public interface IAppReferenceManager
    {
        void AddToProjectLifeCycleReferences(IAppLifeCycleManaged projectLifeCycle);
    }
    public class AppReferenceManager : IAppReferenceManager
    {
        private List<IAppLifeCycleManaged> _projectLifeCycleReferences = new List<IAppLifeCycleManaged>();
        
        public void AddToProjectLifeCycleReferences(IAppLifeCycleManaged projectLifeCycle)
        {
            _projectLifeCycleReferences.Add(projectLifeCycle);
        }
    }
}
