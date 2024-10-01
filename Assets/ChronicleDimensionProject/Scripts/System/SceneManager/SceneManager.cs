using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChronicleDimensionProject.System
{
    public class SceneManager
    {
        private bool _isManagerSceneLoaded = false;
        private const string ManagerSceneName = "ManagerScene";

        // シーンをロードするメソッド
        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
        {
            // ManagerSceneがロードされていない場合、最初にロード
            if (!_isManagerSceneLoaded)
            {
                await LoadManagerSceneAsync();
            }

            // 新しいシーンのロード
            if (!IsSceneLoaded(sceneName))
            {
                var loadSceneOperation =
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
                while (loadSceneOperation is { isDone: false })
                {
                    await UniTask.Yield();
                }

                Debug.Log($"Scene {sceneName} loaded.");
            }
        }

        // ManagerSceneをロードするメソッド
        private async UniTask LoadManagerSceneAsync()
        {
            if (!IsSceneLoaded(ManagerSceneName))
            {
                var loadSceneOperation =
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ManagerSceneName, LoadSceneMode.Additive);
                while (loadSceneOperation is { isDone: false })
                {
                    await UniTask.Yield();
                }

                _isManagerSceneLoaded = true;
                Debug.Log("ManagerScene loaded.");
            }
        }

        // シーンがロードされているか確認するメソッド
        private static bool IsSceneLoaded(string sceneName)
        {
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                {
                    return true;
                }
            }

            return false;
        }

        // ManagerSceneはアンロードできないように制御
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (sceneName == ManagerSceneName)
            {
                Debug.LogWarning("ManagerScene cannot be unloaded.");
                return;
            }

            if (IsSceneLoaded(sceneName))
            {
                var unloadSceneOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
                while (unloadSceneOperation is { isDone: false })
                {
                    await UniTask.Yield();
                }

                Debug.Log($"Scene {sceneName} unloaded.");
            }
        }
    }
}