using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace GameJamProject.SceneManagement
{
    public class SceneLoader
    {
        /// <summary>
        /// シーンを非同期で読み込む
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="loadSceneMode"></param>
        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
        {
            // シーンが読み込まれていない場合は読み込む
            if (!IsSceneLoaded(sceneName))
            {
                var loadSceneOperation =
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
                while (!loadSceneOperation.isDone)
                {
                    await UniTask.Yield();
                }
            }
        }

        /// <summary>
        /// シーンを非同期でアンロードする
        /// </summary>
        /// <param name="sceneName"></param>
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (IsSceneLoaded(sceneName))
            {
                var unloadSceneOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
                while (unloadSceneOperation is { isDone: false })
                {
                    await UniTask.Yield();
                }
            }
        }

        /// <summary>
        /// シーンが読み込まれているかどうか
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public bool IsSceneLoaded(string sceneName)
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
    }
}