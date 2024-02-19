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
        private Scene _unloadScene;
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

            // 最初にロードされたシーンをアンロードしない
            if (_lastScene.IsValid() && _lastScene != _unloadScene)
            {
                await UnloadScene(_lastScene);
            }

            LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, parameters);

            _lastScene = SceneManager.GetSceneByName(scene); // 新しいシーンを_lastSceneとして記録

            return asyncLoad;
        }

        /// <summary> シーンをロードする </summary>
        public void LoadScene(string scene)
        {
            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"{nameof(scene)} は無効です!");

            if (_lastScene.IsValid() && _lastScene != _unloadScene)
            {
                UnloadScene(_lastScene);
            }

            SceneManager.LoadScene(scene);
        }

        /// <summary> シーンをアンロードする </summary>
        private async UniTask UnloadScene(Scene scene)
        {
            if (scene.IsValid())
            {
                await SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}