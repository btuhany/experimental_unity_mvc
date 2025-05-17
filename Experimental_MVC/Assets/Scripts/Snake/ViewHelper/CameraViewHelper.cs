using System;
using Batuhan.EventBus;
using SnakeExample.Events;
using SnakeExample.Grid;
using UnityEngine;
using Zenject;

namespace SnakeExample.View
{
    [RequireComponent(typeof(Camera))]
    public class CameraViewHelper : MonoBehaviour
    {
        [Inject] private GridManager _gridManager;
        [Inject] private IEventBus<GameEvent> _eventBus;

        private void Awake()
        {
            _eventBus.Subscribe<SceneInitializationEvent>(OnSceneInitialization);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<SceneInitializationEvent>(OnSceneInitialization);
        }

        private void OnSceneInitialization(SceneInitializationEvent obj)
        {
            var gridPos = _gridManager.Grid.GetGridCenterWorldPosition();
            transform.position = new Vector3(gridPos.x, gridPos.y, transform.position.z);
        }
    }
}
