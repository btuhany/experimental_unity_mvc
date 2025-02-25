using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;

public class SceneSwitcherWindow : EditorWindow
{
    private Vector2 scrollPos;

    [MenuItem("Batuhan/CustomEditor/SceneQuickSwitcher")]
    public static void ShowWindow()
    {
        var window = GetWindow<SceneSwitcherWindow>();
        window.titleContent = new GUIContent("Scene Switcher");
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Scene Switcher", EditorStyles.boldLabel);
        GUILayout.Space(5);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        var scenes = EditorBuildSettings.scenes;
        if (scenes.Length == 0)
        {
            
        }
        else
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                if (!scenes[i].enabled)
                    continue;

                int sceneIndex = i;
                string scenePath = scenes[i].path;
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);

                GUILayout.BeginHorizontal(EditorStyles.helpBox);

                // Sahne indexi
                GUILayout.Label($"[{sceneIndex}]", GUILayout.Width(30));

                // Sahne geçiş butonu
                if (GUILayout.Button($"{sceneName}", GUILayout.Height(30)))
                {
                    OpenSceneByBuildIndex(sceneIndex);
                }

                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void OpenSceneByBuildIndex(int index)
    {
        var scenes = EditorBuildSettings.scenes;
        if (index < 0 || index >= scenes.Length)
            return;

        var scene = scenes[index];
        if (!scene.enabled)
        {
            return;
        }

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scene.path);
        }
    }
}
