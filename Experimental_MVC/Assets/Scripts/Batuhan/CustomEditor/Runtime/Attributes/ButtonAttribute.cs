using System;
using UnityEngine;

namespace Batuhan.CustomEditor
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string ButtonLabel { get; }

        public ButtonAttribute(string label = null)
        {
            ButtonLabel = label;
        }
    }
}
