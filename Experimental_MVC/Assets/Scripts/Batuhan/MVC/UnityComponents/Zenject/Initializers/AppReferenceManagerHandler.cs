
using Batuhan.MVC.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public interface IAppReferencesManagerHandler
    {
        IAppReferenceManager ReferenceManager { get; }
        List<IAppLifeCycleManaged> ProjectLifeCycleManagedsToAdd { get; }
    }

    /// <summary>
    /// During scene transitions, other objects can be added as app references.
    /// IAppReferenceManager should be resolved from the Project Context.
    /// </summary>
    /// 
    public abstract class AppReferenceManagerHandler : MonoBehaviour, IAppReferencesManagerHandler
    {
        [Inject]
        private IAppReferenceManager _projectReferenceManager;

        [Inject]
        private List<IAppLifeCycleManaged> _projectLifeCycleManagedsToAdd = new List<IAppLifeCycleManaged>();

        public IAppReferenceManager ReferenceManager => _projectReferenceManager;

        public List<IAppLifeCycleManaged> ProjectLifeCycleManagedsToAdd => _projectLifeCycleManagedsToAdd;

        protected virtual void Start()
        {
            UpdateProjectLifeCycleReferences();
        }
        private void UpdateProjectLifeCycleReferences()
        {
            for (int i = 0; i < _projectLifeCycleManagedsToAdd.Count; i++)
            {
                var referenceToAdd = _projectLifeCycleManagedsToAdd[i];
                _projectReferenceManager.AddToProjectLifeCycleReferences(referenceToAdd);
            }
            _projectLifeCycleManagedsToAdd.Clear();
        }
    }
}
