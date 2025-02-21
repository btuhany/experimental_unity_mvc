using Batuhan.MVC.Core;
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
            projectLifeCycle.OnAwakeCallback();
            projectLifeCycle.DestroyDelegate = OnDestroyDelegateInvoked;
        }
        public void OnDestroyDelegateInvoked(IAppLifeCycleManaged appLifeCycleManaged)
        {
            RemoveFromProjectLifeCycleReferences(appLifeCycleManaged);
        }
        private void RemoveFromProjectLifeCycleReferences(IAppLifeCycleManaged appLifeCycleManaged)
        {
            appLifeCycleManaged.OnDestroyCallback();
            _projectLifeCycleReferences?.Remove(appLifeCycleManaged);
        }
        public void InvokeOnDestroyCallbacks()
        {
            for (int i = 0; i < _projectLifeCycleReferences.Count; i++)
            {
                _projectLifeCycleReferences[i].OnDestroyCallback();
            }
        }
    }
}
