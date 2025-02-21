using Batuhan.MVC.Core;
using System.Collections.Generic;

namespace Batuhan.MVC.UnityComponents.Core
{
    public interface ISceneReferenceManager
    {
        void HandleOnAwake();
        void HandleOnStart();
        void HandleOnDestroy();
    }
}