using System;
using TimeCounter.Entities.CounterText;
using TimeCounter.Events.GlobalEvents;
using Zenject;

namespace TimeCounter.Initializer
{
    //TODOBY base controller
    internal class AppInitializerController : IInitializable, IDisposable
    {
        [Inject]
        private IAppInitializerContext _context;

        public void Initialize()
        {
            _context.EventBusGlobal.Publish(new AppInitializedEvent() { Time = UnityEngine.Time.time });
        }

        public void Dispose()
        {
        }

    }
}
