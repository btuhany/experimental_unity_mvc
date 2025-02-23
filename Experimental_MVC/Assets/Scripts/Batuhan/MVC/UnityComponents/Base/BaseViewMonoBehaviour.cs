using Batuhan.MVC.Core;
using System;
using UnityEngine;

namespace Batuhan.MVC.UnityComponents.Base
{
    public abstract class BaseViewMonoBehaviour : MonoBehaviour, IView
    {
        public abstract Type ContractTypeToBind { get; }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
    public abstract class BaseSingletonViewMonoBehaviour<T> : BaseViewMonoBehaviour where T : BaseSingletonViewMonoBehaviour<T>
    {
        private static T _instance;
        protected bool TrySetSingletonAsDDOL(bool setParentNullIfExist = false)
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);

                return false;
            }
            else if (_instance == null)
            {
                _instance = (T) this;

                if (setParentNullIfExist || transform.parent == null)
                {
                    transform.SetParent(null);
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Debug.LogWarning($"Singleton view {nameof(T)} has a gameObject parent! It cannot be marked as DDOL!");
                }
            }
            return true;
        }
    }
}
