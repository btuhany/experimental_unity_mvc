using Batuhan.MVC.Core;
using System;
using UnityEngine;

namespace Assets.Scripts.TimeCounter.Helper
{
    public interface IDebugHelper : IDisposable
    {
        public void Log(string message);
        public void Log(string message, object callerObj = null);
    }
    public class DebugHelper : IDebugHelper
    {
        //TODOBY Conditional attribute
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

        public void Log(string message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }
        public void Dispose()
        {
        }
    }
}
