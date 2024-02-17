using System;
using ChronicleDimensionProject.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChronicleDimensionProject.Boot
{
    public class SceneController : AbstractSingleton<SceneController>
    {
        public static SceneController Instance { get; private set; }

        private Scene _lastScene;

        // 進捗情報を外部に通知するためのAction
        public Action<float> OnLoadProgressChanged { get; set; }

        protected override bool UseDontDestroyOnLoad => true;

        public async UniTask LoadScene(string scene)
        {
            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"{nameof(scene)} は無効です!");

            await UnloadLastScene();
            await LoadSceneAdditive(scene);
        }

        private async UniTask UnloadScene(Scene scene)
        {
            if (!_lastScene.IsValid())
                return;

            await SceneManager.UnloadSceneAsync(scene);
        }

        private async UniTask LoadSceneAdditive(string scene)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                OnLoadProgressChanged?.Invoke(asyncLoad.progress);
                await UniTask.Yield();
            }

            _lastScene = SceneManager.GetSceneByName(scene);
            SceneManager.SetActiveScene(_lastScene);
            OnLoadProgressChanged?.Invoke(1.0f); // ロード完了時に進捗を100%に設定
        }

        public async UniTask UnloadLastScene()
        {
            if (_lastScene.IsValid())
                await UnloadScene(_lastScene);
        }
    }
}