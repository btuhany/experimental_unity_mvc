using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Linq;
using Batuhan.MVC.UnityComponents.Zenject;
using UnityEngine.SceneManagement;

namespace Batuhan.MVC.Editor
{
    public class SceneValidator : EditorWindow
    {
        [MenuItem("Batuhan/MVC/SceneLifeCycleManagerValidator")]
        public static void ShowWindow()
        {
            GetWindow<SceneValidator>("Scene LifeCycle Manager Validator");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Validate All Scenes"))
            {
                ValidateAllScenes();
            }
        }

        private void ValidateAllScenes()
        {
            var currentScenePath = EditorSceneManager.GetActiveScene().path;
            var scenesInBuildExceptCurrent = EditorBuildSettings.scenes
                                                   .Where(scene => scene.enabled && scene.path != currentScenePath)
                                                   .Select(scene => scene.path)
                                                   .ToArray();


            ValidateScene(EditorSceneManager.GetActiveScene());
            foreach (var scenePath in scenesInBuildExceptCurrent)
            {
                ValidateScene(scenePath);
            }
        }

        private void ValidateScene(string scenePath)
        {
            Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
            ValidateScene(scene);
            EditorSceneManager.CloseScene(scene, true);
        }
        private void ValidateScene(Scene scene)
        {
            GameObject[] sceneObjects = scene.GetRootGameObjects();

            var sceneLifeCycleManagers = sceneObjects
                .SelectMany(go => go.GetComponentsInChildren<SceneLifeCycleManager>())
                .ToArray();

            if (sceneLifeCycleManagers.Length > 1)
            {
                Debug.LogError($"Too many SceneLifeCycleManager components found in scene {scene.name}. Please ensure there is only one.");
            }
            else if (sceneLifeCycleManagers.Length == 0)
            {
                Debug.LogWarning($"No SceneLifeCycleManager found in scene {scene.name}. Please add one.");
            }
            else
            {
                Debug.Log($"SceneLifeCycleManager validation passed in scene {scene.name}.");
            }
        }
    }
}
