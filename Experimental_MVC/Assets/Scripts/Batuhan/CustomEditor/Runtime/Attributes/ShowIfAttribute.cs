using System;
using UnityEngine;

namespace Batuhan.CustomEditor
{
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionProperty { get; }

        public ShowIfAttribute(string conditionProperty)
        {
            ConditionProperty = conditionProperty;
        }
    }
}
