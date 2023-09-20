using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class LoadingScene : AbstractSingleton<LoadingScene>
{
    [SerializeField] GameObject loadingUI;
    [SerializeField] Slider slider;

    public async void LoadNextScene(string sceneName)
    {
        loadingUI.SetActive(true);
        await LoadScene(sceneName);
    }

    private async UniTask LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            slider.value = async.progress;
            await UniTask.Yield();
        }

        loadingUI.SetActive(false);
    }
}