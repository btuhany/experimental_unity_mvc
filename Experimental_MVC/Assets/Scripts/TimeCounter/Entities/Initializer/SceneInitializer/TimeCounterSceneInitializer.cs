using TimeCounter.Entities.CounterText;
using UnityEngine;
using Zenject;

namespace TimeCounter.Entities.Initializer
{
    public class TimeCounterSceneInitializer : SceneInitializer<TimeCounterSceneReferenceManager>
    {

    }
    public class TimeCounterSceneReferenceManager : SceneReferenceManager
    {
        [Inject] private CounterTextController _counterTextController;
        public override void HandleOnAwake()
        {
            _counterTextController.Initialize();
        }
        public override void HandleOnDestroy()
        {
            _counterTextController.Dispose();
        }
    }
}
