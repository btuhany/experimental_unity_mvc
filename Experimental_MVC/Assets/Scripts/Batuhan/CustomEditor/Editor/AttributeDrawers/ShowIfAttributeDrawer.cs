using UnityEditor;
using UnityEngine;

namespace Batuhan.CustomEditor.Editor
{
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
  
  
}
