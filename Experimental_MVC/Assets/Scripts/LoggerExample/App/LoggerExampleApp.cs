using Assets.Scripts.LoggerExample.MVC.Entities.CounterLogger;
using UnityEngine;

public class LoggerExampleApp : IAppInitializer
{
    public bool IsInitialized => _isInitialized;

    private bool _isInitialized;
    public void Initialize()
    {
        if (!_isInitialized)
        {
            var counterLogger1 = new CounterLoggerMVCEntity();
            counterLogger1.Initialize();


            _isInitialized = true;
        }
    }
}
