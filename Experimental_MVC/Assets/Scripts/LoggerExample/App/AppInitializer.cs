using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using UnityEngine;
using Zenject;

public class AppInitializer : MonoBehaviour
{
    [Inject] private CounterEntityInitializer _counterInitializer;

    private void Start()
    {
        _counterInitializer.Initialize();
    }
}
