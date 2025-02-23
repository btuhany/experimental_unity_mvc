using Batuhan.MVC.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public enum ReferenceAllocationMode
    {
        Singleton = 0,
        AllowMultiple = 1,
    }
    public interface IAppReferenceManager : IDisposable
    {
        bool AddToAppLifeCycle(IAppLifeCycleManaged projectLifeCycle);
        void OnApplicationQuitCallback();
    }
    /// <summary>
    /// Should be installed at project context to keep just a single persistent object between scenes and at whoel lifetime of the app
    /// </summary>
    public class AppReferenceManager : IAppReferenceManager
    {
        private List<IAppLifeCycleManaged> _appLifeCycleReferences = new List<IAppLifeCycleManaged>();
        
        public bool AddToAppLifeCycle(IAppLifeCycleManaged appLifeCycledObj)
        {
            if (ValidateAllocationMode(appLifeCycledObj))
            {
                _appLifeCycleReferences.Add(appLifeCycledObj);
                appLifeCycledObj.Initialize();
                appLifeCycledObj.RemoveFromAppLifeCycleAction = OnRemoveFromAppLifeCycleCallback;
                return true;
            }
            return false;
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
            RemoveFromAppLifeCycle(appLifeCycleManaged);
        }
        private void RemoveFromAppLifeCycle(IAppLifeCycleManaged appLifeCycleManaged)
        {
            appLifeCycleManaged.Dispose();
            _appLifeCycleReferences?.Remove(appLifeCycleManaged);
        }
        public bool ValidateAllocationMode(IAppLifeCycleManaged appLifeCycleManaged)
        {
            var type = appLifeCycleManaged.AllocationRegistrationType;
            switch (appLifeCycleManaged.AllocationMode)
            {
                case ReferenceAllocationMode.Singleton:
                    {
                        if (_appLifeCycleReferences.Any(x => x.AllocationRegistrationType == type))
                        {
                            return false;
                        }
                    }
                    break;
                case ReferenceAllocationMode.AllowMultiple:
                    break;
            }
            return true;
        }
    }
}