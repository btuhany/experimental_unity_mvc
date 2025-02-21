using Batuhan.MVC.Core;
using System;
using System.Collections.Generic;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public interface IAppReferenceManager : IDisposable
    {
        void AddToProjectLifeCycleReferences(IAppLifeCycleManaged projectLifeCycle);
        void OnDestroyDelegateInvoked(IAppLifeCycleManaged appLifeCycleManaged);
        int GetAppLifeCycleManagedCount();
    }
    public class AppReferenceManager : IAppReferenceManager
    {
        private List<IAppLifeCycleManaged> _projectLifeCycleReferences = new List<IAppLifeCycleManaged>();
        
        public void AddToProjectLifeCycleReferences(IAppLifeCycleManaged projectLifeCycle)
        {
            _projectLifeCycleReferences.Add(projectLifeCycle);
            projectLifeCycle.Initialize();
            projectLifeCycle.DestroyDelegate = OnDestroyDelegateInvoked;
        }
        public void OnDestroyDelegateInvoked(IAppLifeCycleManaged appLifeCycleManaged)
        {
            RemoveFromProjectLifeCycleReferences(appLifeCycleManaged);
        }
        private void RemoveFromProjectLifeCycleReferences(IAppLifeCycleManaged appLifeCycleManaged)
        {
            appLifeCycleManaged.Dispose();
            _projectLifeCycleReferences?.Remove(appLifeCycleManaged);
        }
        public void Dispose()
        {
            for (int i = 0; i < _projectLifeCycleReferences.Count; i++)
            {
                _projectLifeCycleReferences[i].Dispose();
            }
            _projectLifeCycleReferences.Clear();
        }

        public int GetAppLifeCycleManagedCount()
        {
            return _projectLifeCycleReferences.Count;
        }
    }
}
