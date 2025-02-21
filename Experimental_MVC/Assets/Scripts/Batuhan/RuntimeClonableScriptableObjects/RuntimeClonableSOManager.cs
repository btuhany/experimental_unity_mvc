using Batuhan.RuntimeClonableScriptableObjects;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Batuhan.RuntimeCopyScriptableObjects
{
    public class RuntimeClonableSOManager : MonoBehaviour
    {
#if UNITY_EDITOR
        private const string RUNTIME_CLONES_FOLDER_PATH = "Assets/RuntimeResources";
        private const string FOLDER_NAME = "ScriptableObjects";
        private const string FULL_PATH = "Assets/RuntimeResources/ScriptableObjects";
        private const string INSTATIATED_SO_SUFFIX = "_RuntimeResource.asset";
        private bool _isRegisteredToEditorAppEvent = false;
        private List<RuntimeClonableScriptableObject> _runtimeClonedBaseSOList = new List<RuntimeClonableScriptableObject>();
        private void Awake()
        {
            if (!_isRegisteredToEditorAppEvent)
            {
                EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
                _isRegisteredToEditorAppEvent = true;
            }
        }
        private void OnDestroy()
        {
            if (_isRegisteredToEditorAppEvent)
            {
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            }
        }
        private void OnPlayModeStateChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingPlayMode)
            {
                DeleteAllRuntimeSOAssets();
            }
        }
        public void DeleteAllRuntimeSOAssets()
        {
            if (!AssetDatabase.IsValidFolder(FULL_PATH))
            {
                Debug.LogWarning($"Couldn't find any runtime so in {FULL_PATH}. There isn't anything to cleanup!");
                return;
            }

            string[] assetPaths = Directory.GetFiles(FULL_PATH, "*.asset");

            foreach (string assetPath in assetPaths)
            {
                string unityPath = assetPath.Replace("\\", "/");
                bool deleted = AssetDatabase.DeleteAsset(unityPath);

                if (deleted)
                    Debug.Log($"Deleted: {unityPath}");
                else
                    Debug.LogWarning($"Couldn't be deleted: {unityPath}");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            foreach (var so in _runtimeClonedBaseSOList)
            {
                so.RuntimeClone = null;
            }
            _runtimeClonedBaseSOList.Clear();
        }
#endif
        public T CreateModelDataSOInstance<T>(T baseData) where T : RuntimeClonableScriptableObject
        {
            T copyData = Instantiate(baseData);

#if UNITY_EDITOR
            
            if (!AssetDatabase.IsValidFolder(FULL_PATH))
            {
                AssetDatabase.CreateFolder(RUNTIME_CLONES_FOLDER_PATH, FOLDER_NAME);
                Debug.Log($"Created folder {FOLDER_NAME} for runtime so assets in: {RUNTIME_CLONES_FOLDER_PATH}");
            }

            var assetName = $"{baseData.name}{ INSTATIATED_SO_SUFFIX}";
            string assetPath = $"{FULL_PATH}/" + assetName;

            AssetDatabase.CreateAsset(copyData, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            baseData.RuntimeClone = copyData;
            _runtimeClonedBaseSOList.Add(baseData);

            copyData.EDITOR_ShowRuntimeClone = false;
            Debug.Log($"Created runtime so asset {assetName}");
#endif

            return copyData;
        }
       
    }
}
