using System;
using UnityEditor;
using UnityEngine;

namespace Batuhan.RuntimeClonableScriptableObjects
{
    public class BaseRuntimeClonableScriptableObject : ScriptableObject
    {
#if UNITY_EDITOR
        [HideInInspector]
        public bool EDITOR_ShowRuntimeClone = true;
        [ShowIf("EDITOR_ShowRuntimeClone")]
        public ScriptableObject RuntimeClone;
#endif
    }

    /* THANKS CHATGPT */
#if UNITY_EDITOR
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionProperty { get; }

        public ShowIfAttribute(string conditionProperty)
        {
            ConditionProperty = conditionProperty;
        }
    }

    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionProperty);

            if (conditionProperty != null && conditionProperty.boolValue)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionProperty);

            if (conditionProperty != null && conditionProperty.boolValue)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            return 0;
        }
    }
#endif
}
