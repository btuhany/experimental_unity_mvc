using UnityEngine;
using UnityEditor;
using Batuhan.GridSystem.Core;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.Experimental;

namespace Batuhan.GridSystem.Editor
{
    public abstract class AbstractGridDebuggerWindow<T> : EditorWindow
    {
        protected IGridProvider<T> _gridProvider;
        private Vector2 scrollPos;
        private UnityEngine.Object _selectedGridProviderObj;
        private bool _isEnabled;
        private string _cachedGlobalObjectId;
        private const string CACHE_KEY = "GridDebugger_CachedGlobalObjectId";

        protected virtual int GetGUILayoutWidth() => 40;
        protected virtual int GetGUILayoutHeight() => 40;

        protected virtual void OnGUI()
        {
            GUILayout.Label("Grid Debugger", EditorStyles.boldLabel);

            bool newDebugState = EditorGUILayout.Toggle("Enable Debugging", _isEnabled);
            if (newDebugState != _isEnabled)
            {
                _isEnabled = newDebugState;
            }

            _selectedGridProviderObj = EditorGUILayout.ObjectField("Grid Provider", _selectedGridProviderObj, typeof(MonoBehaviour), true);

            if (_selectedGridProviderObj != null)
            {
                _gridProvider = _selectedGridProviderObj as IGridProvider<T>;
                if (_gridProvider != null)
                {
                    _cachedGlobalObjectId = GlobalObjectId.GetGlobalObjectIdSlow(_selectedGridProviderObj).ToString();
                    SessionState.SetString(CACHE_KEY, _cachedGlobalObjectId);
                }
            }

            if (_gridProvider == null)
            {
                EditorGUILayout.HelpBox("Please select the grid provider in the scene!", MessageType.Warning);
                return;
            }

            if (EditorApplication.isPlaying)
            {
                Grid2D<T> gridModel = _gridProvider.GetGridModel();
                if (gridModel == null)
                {
                    EditorGUILayout.HelpBox("Grid couldn't be found!", MessageType.Warning);
                    return;
                }
                if (gridModel != null && _isEnabled)
                {
                    DrawGrid(gridModel);
                }
            }
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorSceneManager.sceneOpened += OnSceneOpened;

            RestoreCachedGridProvider();
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorSceneManager.sceneOpened -= OnSceneOpened;
        }

        private void RestoreCachedGridProvider()
        {
            _cachedGlobalObjectId = SessionState.GetString(CACHE_KEY, string.Empty);
            if (!string.IsNullOrEmpty(_cachedGlobalObjectId))
            {
                if (GlobalObjectId.TryParse(_cachedGlobalObjectId, out var id))
                {
                    _selectedGridProviderObj = GlobalObjectId.GlobalObjectIdentifierToObjectSlow(id);
                    _gridProvider = _selectedGridProviderObj as IGridProvider<T>;
                }
            }
        }

        private void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            RestoreCachedGridProvider();
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    RestoreCachedGridProvider();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    if (_selectedGridProviderObj != null)
                    {
                        _cachedGlobalObjectId = GlobalObjectId.GetGlobalObjectIdSlow(_selectedGridProviderObj).ToString();
                        SessionState.SetString(CACHE_KEY, _cachedGlobalObjectId);
                    }
                    break;
            }
        }

        private void DrawGrid(Grid2D<T> grid)
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            for (int y = grid.Height - 1; y >= 0; y--)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < grid.Width; x++)
                {
                    T cellData = grid.GetElement(x, y);
                    string cellValue = cellData != null ? GetCellDisplayValue(cellData) : "null";
                    GUILayout.Box(cellValue, GUILayout.Width(GetGUILayoutWidth()), GUILayout.Height(GetGUILayoutHeight()));
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            Repaint();
        }

        protected abstract string GetCellDisplayValue(T cellData);
    }
}
