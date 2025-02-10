using Assets.Scripts.LoggerExample.MVC.Entities.Circle;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LoggerExample
{
    internal class TEMP_CircleSpawner : MonoBehaviour
    {
        CircleController.Factory _circleFactory;

        [Inject]
        public void Construct(CircleController.Factory circleFactory)
        {
            _circleFactory = circleFactory;
        }

        [ContextMenu("Spawn Circle")]
        public void SpawnCircle()
        {
            _circleFactory.Create();
        }
    }
}
