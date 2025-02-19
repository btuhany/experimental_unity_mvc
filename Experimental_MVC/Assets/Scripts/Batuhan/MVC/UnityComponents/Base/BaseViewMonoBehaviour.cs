using Batuhan.MVC.Core;
using System;
using UnityEngine;

namespace Batuhan.MVC.UnityComponents.Base
{
    public abstract class BaseViewMonoBehaviour : MonoBehaviour 
    {
        public abstract Type ContractTypeToBind { get; }
    }
}
