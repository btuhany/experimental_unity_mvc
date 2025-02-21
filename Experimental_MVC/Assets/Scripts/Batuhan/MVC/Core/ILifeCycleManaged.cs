using System;

namespace Batuhan.MVC.Core
{
    public interface ISceneLifeCycleManaged //: IDisposable //ILifeCycleAware, IHasLifeCycle, ILifeCycleTrackable
    {
        /// <summary>
        /// Initializes the object or component. This method is typically called when the object is created or activated.
        /// </summary>
        void OnAwakeCallback();
        /// <summary>
        /// Disposes of the object or component. This method is typically called when the object is destroyed or deactivated.
        /// </summary>
        void OnDestroyCallback();
    }
}
