using Batuhan.MVC.Core;
using System;
using System.Collections.Generic;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public interface IAppReferenceManager : IDisposable
    {
        void AddToAppLifeCycle(IAppLifeCycleManaged projectLifeCycle);
        int GetAppLifeCycleManagedCount();
        void OnApplicationQuitCallback();
    }
    /// <summary>
    /// Should be installed at project context to keep just a single persistent object between scenes.
    /// </summary>
    public class AppReferenceManager : IAppReferenceManager
    {
        private List<IAppLifeCycleManaged> _projectLifeCycleReferences = new List<IAppLifeCycleManaged>();
        
        public void AddToAppLifeCycle(IAppLifeCycleManaged projectLifeCycle)
        {
            _projectLifeCycleReferences.Add(projectLifeCycle);
            projectLifeCycle.Initialize();
            projectLifeCycle.RemoveFromAppLifeCycleAction = OnRemoveFromAppLifeCycleCallback;
        }
        public void OnApplicationQuitCallback()
        {
            Dispose();
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
        private void OnRemoveFromAppLifeCycleCallback(IAppLifeCycleManaged appLifeCycleManaged)
        {
            RemoveFromProjectLifeCycle(appLifeCycleManaged);
        }
        private void RemoveFromProjectLifeCycle(IAppLifeCycleManaged appLifeCycleManaged)
        {
            appLifeCycleManaged.Dispose();
            _projectLifeCycleReferences?.Remove(appLifeCycleManaged);
        }
    }
}
