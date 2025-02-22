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
            EditorGUILayout.Space();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawMonoLifeCycleManager();
            EditorGUILayout.EndVertical();
            DrawSeperationLine();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawHeader($"ALL VIEW MONOBEHAVIOURS (IView) IN SCENE");
            DrawInspectorOfGameObjectCastType<IView>();
            EditorGUILayout.EndVertical();
            DrawSeperationLine();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawHeader($"ALL MODEL MONOBEHAVIOURS (IModel) IN SCENE");
            DrawInspectorOfGameObjectCastType<IModel>();
            EditorGUILayout.EndVertical();
            DrawSeperationLine();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawHeader($"ALL CONTROLLER MONOBEHAVIOURS (IController) IN SCENE");
            DrawInspectorOfGameObjectCastType<IController>();
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();
        }

        private static void DrawSeperationLine()
        {
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
            EditorGUILayout.Space();
        }

        private void DrawInspectorOfGameObjectCastType<T>()
        {
            var allViews = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                            .Where(m => m is T)
                            .Cast<T>()
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
                EditorGUILayout.LabelField($"There is no {typeof(T)} in scene.");
            }
        }

        private void ShowObject(GameObject gameObject)
        {
            bool isDDOL = IsDontDestroyOnLoad(gameObject);
            string label = gameObject.name;
            if (isDDOL)
            {
                label += " [DDOL]";
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.MaxWidth(200));

            GUILayout.Space(10);

            bool oldEnabled = GUI.enabled;
            GUI.enabled = false;
            EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);
            GUI.enabled = oldEnabled;

            EditorGUILayout.EndHorizontal();
        }

        private bool IsDontDestroyOnLoad(GameObject obj)
        {
            return obj.scene.name == "DontDestroyOnLoad" || obj.scene.buildIndex == -1;
        }
        private void DrawMonoLifeCycleManager()
        {
            DrawHeader("SCENE LIFE CYCLE MANAGER INSPECTOR");

            if (_sceneLifeCycleManager == null)
            {
                EditorGUILayout.LabelField("Manager does not exist!");
                return;
            }


            var injectedFields = GetInjectedFields(_sceneLifeCycleManager);
            AppReferenceManager appReferenceManager = null;
            if (injectedFields != null)
            {
                foreach (var field in injectedFields)
                {
                    object value = field.GetValue(_sceneLifeCycleManager);

                    if (field.FieldType == typeof(IAppReferenceManager))
                    {
                        appReferenceManager = value as AppReferenceManager;
                    }
                    else 
                    {
                        ShowField(field.Name, value);
                    }
                }
            }

            ShowField("App References That Added From This Scene", _sceneLifeCycleManager.AppReferencesAddedToAppLifeCycle, new GUIStyleState() { textColor = Color.magenta});
            DrawAppLifeCycleManager(appReferenceManager);
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
        private void ShowField(string fieldName, object value, GUIStyleState overrideStyleState = null)
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
                fontStyle = FontStyle.BoldAndItalic,
                alignment = TextAnchor.MiddleLeft
            };
            EditorGUILayout.LabelField(fieldName, headerStyle);

            if (value is IEnumerable collection)
            {
                int count = 0;
                foreach (var item in collection)
                {
                    count++;
                    ShowFieldObject(item, overrideStyleState);
                }
                if (count == 0)
                {
                    EditorGUILayout.LabelField("- There is no object");
                }
            }
            else
            {
                ShowFieldObject(value);
            }
        }

        private void DrawAppLifeCycleManager(AppReferenceManager appManager)
        {
            DrawHeader("APP LIFE CYCLE MANAGER INSPECTOR");

            if (appManager == null)
            {
                EditorGUILayout.LabelField("Manager does not exist!");
                return;
            }

            FieldInfo field = typeof(AppReferenceManager)
                    .GetField("_appLifeCycleReferences", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                var references = field.GetValue(appManager) as IEnumerable<IAppLifeCycleManaged>;

                if (references != null)
                {
                    foreach (var reference in references)
                    {
                        ShowFieldObject(reference);
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
        private void ShowFieldObject(object obj, GUIStyleState overrideInstanceStyleState = null)
        {
            var nullStyle = new GUIStyle()
            {
                fontSize = 14,
                normal = { textColor = Color.magenta }
            };

            var instanceStyle = new GUIStyle()
            {
                fontSize = 14,
                normal = { textColor = Color.white }
            };
            if (overrideInstanceStyleState != null)
            {
                instanceStyle.normal = overrideInstanceStyleState;
            }

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
                EditorGUILayout.LabelField($"- {obj.GetType().Name}", instanceStyle);
            }
        }
    }
}
