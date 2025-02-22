using Batuhan.MVC.Core;
using Batuhan.MVC.UnityComponents.Zenject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Batuhan.MVC.Editor
{
    public class LifeCycleManagerEditor : UnityEditor.EditorWindow
    {
        private Vector2 _scrollPosition;
        private SceneLifeCycleManager _sceneLifeCycleManager;

        [MenuItem("Batuhan.MVC/Debug/LifeCycleInspector")]
        public static void ShowWindow()
        {
            LifeCycleManagerEditor window = GetWindow<LifeCycleManagerEditor>("Life Cycle Inspector");
            window.Show();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;

            _sceneLifeCycleManager = FindFirstObjectByType<SceneLifeCycleManager>();
            if (_sceneLifeCycleManager == null)
            {
                Debug.LogWarning("Manager does not exist!");
            }
        }
        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            _sceneLifeCycleManager = FindFirstObjectByType<SceneLifeCycleManager>();
        }
        private void OnPlayModeStateChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.EnteredPlayMode)
            {
                _sceneLifeCycleManager = FindFirstObjectByType<SceneLifeCycleManager>();
                if (_sceneLifeCycleManager == null)
                {
                    Debug.LogWarning("-----------------!");
                }
            }
            else if (change == PlayModeStateChange.ExitingPlayMode)
            {
                _sceneLifeCycleManager = null;
            }
        }
        private void OnGUI()
        {
            DrawHeader("LIFE CYCLE INSPECTOR");

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            DrawMonoLifeCycleManager();

            DrawSeperationLine();

            DrawIViewInspector();

            EditorGUILayout.EndScrollView();
        }

        private static void DrawSeperationLine()
        {
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2), Color.gray);
            EditorGUILayout.Space();
        }

        private void DrawIViewInspector()
        {
            DrawHeader("ALL VIEWS (IView) IN SCENE");

            var allViews = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                            .Where(m => m is IView)
                            .Cast<IView>()
                            .ToList();

            EditorGUILayout.Space();

            if (allViews.Any())
            {
                foreach (var view in allViews)
                {
                    ShowObject((view as MonoBehaviour).gameObject);
                }
            }
            else
            {
                EditorGUILayout.LabelField("There is no IView in scene.");
            }
        }
        private void ShowObject(GameObject gameObject)
        {
            bool guiState = GUI.enabled;
            GUI.enabled = false;

            EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);

            GUI.enabled = guiState;
        }
        private void DrawMonoLifeCycleManager()
        {
            GUIStyle subHeader = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 12,
                normal = { textColor = Color.white },
            };

            EditorGUILayout.LabelField("From Scene Life Cycle Manager:", subHeader);

            if (_sceneLifeCycleManager == null)
            {
                EditorGUILayout.LabelField("Manager does not exist!");
                return;
            }


            var injectedFields = GetInjectedFields(_sceneLifeCycleManager);
            if (injectedFields != null)
            {
                foreach (var field in injectedFields)
                {
                    object value = field.GetValue(_sceneLifeCycleManager);
                    ShowInjectedField(field.Name, value);
                }
            }
        }

        private void DrawHeader(string header)
        {
            GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 16,
                normal = { textColor = Color.white },
                alignment = TextAnchor.MiddleCenter
            };

            EditorGUILayout.LabelField(header, headerStyle);
        }
        private FieldInfo[] GetInjectedFields(object obj)
        {
            return obj.GetType()
                      .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                      .Where(f => f.GetCustomAttribute(typeof(InjectAttribute)) != null)
                      .ToArray();
        }
        private void ShowInjectedField(string fieldName, object value)
        {
            fieldName = SplitCamelCase(fieldName);
            if (fieldName.StartsWith("_"))
            {
                fieldName = fieldName.Substring(1);
            }
            fieldName = fieldName.ToUpperInvariant();

            var headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14,
                normal = { textColor = Color.green }
            };
            EditorGUILayout.LabelField(fieldName, headerStyle);

            if (value is IEnumerable collection)
            {
                int count = 0;
                foreach (var item in collection)
                {
                    count++;
                    ShowObject(item);
                }
                if (count == 0)
                {
                    EditorGUILayout.LabelField("- There is no object");
                }
            }
            else if (value is AppReferenceManager appManager)
            {
                ShowAppLifeCycleReferences(appManager);
            }
            else
            {
                ShowObject(value);
            }
        }

        private void ShowAppLifeCycleReferences(AppReferenceManager appManager)
        {
            FieldInfo field = typeof(AppReferenceManager)
                    .GetField("_appLifeCycleReferences", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                var references = field.GetValue(appManager) as IEnumerable<IAppLifeCycleManaged>;
                EditorGUILayout.LabelField("App Life Cycle References", EditorStyles.boldLabel);

                if (references != null)
                {
                    foreach (var reference in references)
                    {
                        ShowObject(reference);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("- NULL");
                }
            }
        }

        private string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
        }
        private void ShowObject(object obj)
        {
            var nullStyle = new GUIStyle()
            {
                fontSize = 14,
                normal = { textColor = Color.red }
            };

            var instanceStyle = new GUIStyle()
            {
                fontSize = 14,
                normal = { textColor = Color.white }
            };

            if (obj == null)
            {
                EditorGUILayout.LabelField("- NULL", nullStyle);
            }
            else if (obj is UnityEngine.Object unityObj)
            {
                EditorGUILayout.ObjectField(unityObj, typeof(UnityEngine.Object), true);
            }
            else
            {
                EditorGUILayout.LabelField($"- {obj.GetType().FullName}", instanceStyle);
            }
        }
    }
}
