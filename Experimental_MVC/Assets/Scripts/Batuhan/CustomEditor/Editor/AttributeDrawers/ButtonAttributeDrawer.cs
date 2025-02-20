using System.Reflection;
using UnityEditor;
using Batuhan.CustomEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(UnityEngine.Object), true)]
public class ButtonEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var type = target.GetType();
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

        foreach (var method in methods)
        {
            var attribute = (ButtonAttribute)System.Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

            if (attribute != null)
            {
                string buttonLabel = string.IsNullOrEmpty(attribute.ButtonLabel) ? method.Name : attribute.ButtonLabel;

                if (GUILayout.Button(buttonLabel))
                {
                    method.Invoke(target, null);
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}