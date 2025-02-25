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

            if (GUILayout.Button("Add SceneLifeCycleManager to Active Scene"))
            {
                AddSceneLifeCycleManager();
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
            var managerCount = GetSceneLifeCycleManagersCount(scene);
            if (managerCount > 1)
            {
                Debug.LogError($"Too many SceneLifeCycleManager components found in scene {scene.name}. Please ensure there is only one.");
            }
            else if (managerCount == 0)
            {
                Debug.LogWarning($"No SceneLifeCycleManager found in scene {scene.name}. Please add one.");
            }
            else
            {
                Debug.Log($"SceneLifeCycleManager validation passed in scene {scene.name}.");
            }
        }
        private void AddSceneLifeCycleManager()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            var managerCount = GetSceneLifeCycleManagersCount(activeScene);

            if (managerCount > 0)
            {
                Debug.LogWarning($"Scene {activeScene.name} already contains a SceneLifeCycleManager. No new one was added.");
                return;
            }

            GameObject managerObject = new GameObject("SceneLifeCycleManager");
            managerObject.AddComponent<SceneLifeCycleManager>();

            Undo.RegisterCreatedObjectUndo(managerObject, "Added SceneLifeCycleManager");

            Debug.Log($"SceneLifeCycleManager added to active scene: {activeScene.name}");
        }
        private int GetSceneLifeCycleManagersCount(Scene scene)
        {
            GameObject[] sceneObjects = scene.GetRootGameObjects();
            var sceneLifeCycleManagers = sceneObjects
                .SelectMany(go => go.GetComponentsInChildren<SceneLifeCycleManager>())
                .ToArray();
            return sceneLifeCycleManagers.Length;
        }
    }
}
