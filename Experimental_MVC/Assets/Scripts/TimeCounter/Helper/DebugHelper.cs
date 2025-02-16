using Batuhan.MVC.Core;
using UnityEngine;

namespace Assets.Scripts.TimeCounter.Helper
{
    internal class DebugHelper
    {
        public void Log(string message, object callerObj = null)
        {
#if UNITY_EDITOR
            var logStr = string.Empty;

            if (callerObj is not null)
            {
                switch (callerObj)
                {
                    case IController:
                        logStr = "CONTROLLER | ";
                        break;
                    case IModel:
                        logStr = "MODEL | ";
                        break;
                    case IView:
                        logStr = "VIEW | ";
                        break;
                }
                logStr += callerObj.GetType().Name;
            }

            logStr += " " + message;
            Debug.Log(logStr);
#endif
        }
    }
}
