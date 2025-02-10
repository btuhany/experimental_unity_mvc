using Assets.Scripts.LoggerExample.Installers.ScriptableObjects;
using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using UnityEngine;
using Zenject;

public class AppInitializer : MonoBehaviour
{
    //TODOby: entry point with event manager
    [Inject] private CounterEntityInitializer _counterInitializer;

    private void Start()
    {
        _counterInitializer.Initialize();

    }
}
