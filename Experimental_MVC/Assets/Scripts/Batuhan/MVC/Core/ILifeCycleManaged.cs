namespace Batuhan.MVC.Core
{
    public interface ILifeCycleHandler //: IDisposable //ILifeCycleAware, IHasLifeCycle, ILifeCycleTrackable
    {
        /// <summary>
        /// Initializes the object or component. This method is typically called when the object is created or activated.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Disposes of the object or component. This method is typically called when the object is destroyed or deactivated.
        /// </summary>
        void Dispose();
    }
}
