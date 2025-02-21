using Batuhan.MVC.UnityComponents.Zenject;
using UnityEngine;

namespace ExperimentalMVC.App
{
    public class ExperimentalMVCReferenceManagerHandler : AppReferenceManagerHandler
    {
        protected override void Start()
        {
            base.Start();
            var refManager = ReferenceManager as ExperimentalMVCReferenceManager;
            if (ReferenceManager == null)
            {
                Debug.Log("REF MANAGER IS NULL");
                return;
            }
            if (refManager.randomInt == 0)
            {
                refManager.randomInt = Random.Range(2, 100);
                Debug.Log("random int set: " + refManager.randomInt);
            }
            Debug.Log("random int get: " + refManager.randomInt);
            Debug.Log("App cycle managed count: " + refManager.GetAppLifeCycleManagedCount());
        }
    }
}
