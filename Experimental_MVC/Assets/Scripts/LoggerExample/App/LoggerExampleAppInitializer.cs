using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using UnityEngine;

//TODO: Seperate App Initializer logic which gets a instance of the app (NON-MONOBEHAVIOUR)
public class LoggerExampleAppInitializer : MonoBehaviour, IAppInitializer
{
    [SerializeField] private CounterView _counterView;

    public bool IsInitialized => _isInitialized;

    private bool _isInitialized;
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        if (!_isInitialized)
        {
            var counter = new CounterMVCEntity(_counterView);
            counter.Initialize();

            //var logger = new LoggerMVCEntity();
            //logger.Initialize();

            _isInitialized = true;
        }
    }
}
