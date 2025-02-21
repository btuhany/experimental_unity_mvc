
using Batuhan.MVC.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Batuhan.MVC.UnityComponents.Zenject
{
    public abstract class ProjectReferencesInitializer : MonoBehaviour
    {
        [Inject]
        private ProjectReferenceManager _projectReferenceManager;

        [Inject]
        private List<IProjectLifeCycleManaged> _projectLifeCycleManagedsToAdd;
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
