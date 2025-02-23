using Batuhan.MVC.Core;
using System;
using System.Collections.Generic;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public interface IAppReferenceManager : IDisposable
    {
        void AddToAppLifeCycle(IAppLifeCycleManaged projectLifeCycle);
        void OnApplicationQuitCallback();
    }
    /// <summary>
    /// Should be installed at project context to keep just a single persistent object between scenes and at whoel lifetime of the app
    /// </summary>
    public class AppReferenceManager : IAppReferenceManager
    {
        private List<IAppLifeCycleManaged> _appLifeCycleReferences = new List<IAppLifeCycleManaged>();
        
        public void AddToAppLifeCycle(IAppLifeCycleManaged projectLifeCycle)
        {
            _appLifeCycleReferences.Add(projectLifeCycle);
            projectLifeCycle.Initialize();
            projectLifeCycle.RemoveFromAppLifeCycleAction = OnRemoveFromAppLifeCycleCallback;
        }
        public void OnApplicationQuitCallback()
        {
            Dispose();
        }
        public void Dispose()
        {
            for (int i = 0; i < _appLifeCycleReferences.Count; i++)
            {
                _appLifeCycleReferences[i].Dispose();
            }
            _appLifeCycleReferences.Clear();
        }
        private void OnRemoveFromAppLifeCycleCallback(IAppLifeCycleManaged appLifeCycleManaged)
        {
            RemoveFromProjectLifeCycle(appLifeCycleManaged);
        }
        private void RemoveFromProjectLifeCycle(IAppLifeCycleManaged appLifeCycleManaged)
        {
            appLifeCycleManaged.Dispose();
            _appLifeCycleReferences?.Remove(appLifeCycleManaged);
        }
    }
}
