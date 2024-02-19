using System;
using System.Threading.Tasks;
using ChronicleDimensionProject.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChronicleDimensionProject.Boot
{
    public class SceneController : AbstractSingleton<SceneController>
    {
        // リードオンリー

        private Scene _unloadScene; // アンロードしないシーン
        private Scene _lastScene;

        protected override void OnAwake()
        {
            _unloadScene = SceneManager.GetActiveScene();
        }

        /// <summary> シーンを非同期でロードする </summary>
        public async UniTask<AsyncOperation> LoadSceneAsync(string scene)
        {
            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"{nameof(scene)} は無効です!");

            await UnloadScene(_lastScene);

            LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, parameters);

            _lastScene = SceneManager.GetSceneByName(scene); // 新しいシーンを_lastSceneとして記録

            return asyncLoad;
        }

        /// <summary> シーンをロードする </summary>
        public async void LoadScene(string scene)
        {
            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"{nameof(scene)} は無効です!");
            await UnloadScene(_lastScene);

            SceneManager.LoadScene(scene);
            _lastScene = SceneManager.GetSceneByName(scene);
        }

        /// <summary> シーンをアンロードする </summary>
        private async UniTask UnloadScene(Scene scene)
        {
            if (scene != _unloadScene && scene.IsValid())
            {
                await SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}