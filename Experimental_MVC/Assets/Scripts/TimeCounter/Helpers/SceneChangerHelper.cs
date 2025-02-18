using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangerHelper : MonoBehaviour
{
    [SerializeField] private int _nextSceneIndex;
    [SerializeField] private Button _button;
    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ChangeScene(_nextSceneIndex);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public async UniTaskVoid LoadSceneAsyncByIndex(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress < 0.9f) // Yükleme %90'a ulaþana kadar bekle
            {
                Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            }
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            await UniTask.Yield();
        }
    }

}
