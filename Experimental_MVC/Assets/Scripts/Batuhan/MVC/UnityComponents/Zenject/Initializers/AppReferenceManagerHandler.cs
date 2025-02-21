
using Batuhan.MVC.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public interface IAppReferencesInitializer
    {
        IAppReferenceManager ReferenceManager { get; }
        List<IAppLifeCycleManaged> ProjectLifeCycleManagedsToAdd { get; }
    }
    public abstract class AppReferenceManagerHandler : MonoBehaviour, IAppReferencesInitializer
    {
        [Inject]
        private IAppReferenceManager _projectReferenceManager;

        [Inject]
        private List<IAppLifeCycleManaged> _projectLifeCycleManagedsToAdd;

        public IAppReferenceManager ReferenceManager => _projectReferenceManager;

        public List<IAppLifeCycleManaged> ProjectLifeCycleManagedsToAdd => _projectLifeCycleManagedsToAdd;

        private void Awake()
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
