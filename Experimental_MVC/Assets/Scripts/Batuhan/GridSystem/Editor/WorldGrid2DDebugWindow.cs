using UnityEngine;
using UnityEditor;
using Batuhan.GridSystem.WorldGrid;

/// <summary>
/// The World Grid Debug Window allows debugging of any object implementing 
/// the `IWorldGridDebuggable` interface. It provides options to enable or disable 
/// debugging dynamically while in Play Mode.
/// </summary>
public class WorldGrid2DDebugWindow : EditorWindow
{
    private static IWorldGridDebuggable _target;
    private static WorldGridDebugDrawer _drawer;
    private bool _isEnabled;

    [MenuItem("Batuhan/Grid System/World Grid Debugger")]
    public static void ShowWindow()
    {
        GetWindow<WorldGrid2DDebugWindow>("World Grid Debugger");
    }
    private void OnGUI()
    {
        GUILayout.Label("World Grid Debugger", EditorStyles.boldLabel);

        bool newDebugState = EditorGUILayout.Toggle("Enable Debugging", _isEnabled);
        if (newDebugState != _isEnabled)
        {
            _isEnabled = newDebugState;
            if (EditorApplication.isPlaying)
            {
                CheckHandleDebugging();
            }
        }

        if (EditorApplication.isPlaying)
        {
            if (_target != null && _isEnabled)
            {
                DrawGridStatus();
            }
        }
    }
    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }
    private void CheckHandleDebugging()
    {
        if (_isEnabled)
            EnableDebugging();
        else
            DisableDebugging();
    }
    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.EnteredPlayMode:
                CheckHandleDebugging();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                DisableDebugging();
                break;
        }
    }

    private void FindDebuggableTarget()
    {
        if (_target != null)
            return;

        MonoBehaviour[] sceneObjects = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (MonoBehaviour obj in sceneObjects)
        {
            if (obj is IWorldGridDebuggable debuggable)
            {
                _target = debuggable;
                break;
            }
        }
    }
    private void EnableDebugging()
    {
        if (_target == null)
            FindDebuggableTarget();

        if (_target != null)
        {
            _drawer = new WorldGridDebugDrawer(_target);
            _drawer.ToggleDebug(true);
        }
    }

    private void DisableDebugging()
    {
        if (_drawer != null)
        {
            _drawer.ToggleDebug(false);
            _drawer.Cleanup();
        }
        _target = null;
    }
    private void DrawGridStatus()
    {
        if (_target.IsReady)
        {
            int width = _target.Width;
            int height = _target.Height;

            //TODOBY order option 
            for (int y = height - 1; y >= 0; y--)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < width; x++)
                {
                    string cellStatus = _target.IsCellOccupied(x, y) ? _target.GetCellOccuppiedStr(x, y) : "null";
                    GUILayout.Box(cellStatus, GUILayout.Width(40), GUILayout.Height(40));
                }
                GUILayout.EndHorizontal();
            }
            Repaint();
        }
    }
}
