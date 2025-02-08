using UnityEngine;
namespace Assets.Scripts.LoggerExample.Unity
{
    public class AppInitializer : MonoBehaviour
    {
        private void Awake()
        {
            var loggerExampleApp = new LoggerExampleApp();
            loggerExampleApp.Initialize();
        }
    }
}
