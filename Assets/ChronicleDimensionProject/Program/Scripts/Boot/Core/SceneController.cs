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
        private Scene _neverUnloadScene; // アンロードしないシーン
        private Scene _lastScene;
        protected override bool UseDontDestroyOnLoad => true;

        protected override void OnAwake()
        {
            _neverUnloadScene = SceneManager.GetActiveScene();
            _lastScene = _neverUnloadScene;
        }

        /// <summary> シーンを非同期でロードする </summary>
        public async UniTask<AsyncOperation> LoadSceneAsync(string scene)
        {
            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"{nameof(scene)} は無効です!");

            await UnloadLastScene();

            LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, parameters);

            _lastScene = SceneManager.GetSceneByName(scene); // 新しいシーンを_lastSceneとして記録
            SceneManager.SetActiveScene(_lastScene);
            return asyncLoad;
        }

        /// <summary> シーンをロードする </summary>
        public async void LoadScene(string scene)
        {
            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"{nameof(scene)} は無効です!");
            await UnloadLastScene();

            await LoadSceneAdditive(scene);
        }

        public async void LoadNewScene(string scene)
        {
            if (string.IsNullOrEmpty(scene))
                throw new ArgumentException($"{nameof(scene)} は無効です!");
            await UnloadLastScene();

            LoadNewSceneAdditive(scene);
        }

        private void LoadNewSceneAdditive(string sceneName)
        {
            var scene = SceneManager.CreateScene(sceneName);
            SceneManager.SetActiveScene(scene);
            _lastScene = scene;
        }

        private async UniTask LoadSceneAdditive(string scene)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            await asyncLoad;

            _lastScene = SceneManager.GetSceneByName(scene);
            SceneManager.SetActiveScene(_lastScene);

            // シーンが完全に読み込まれるまで待機する
            while (!_lastScene.isLoaded)
            {
                await UniTask.Yield();
            }
        }

        /// <summary> 最後にロードされたシーンを非同期でアンロードします。 </summary>
        private async UniTask UnloadLastScene()
        {
            if (_lastScene != _neverUnloadScene)
            {
                await UnloadScene(_lastScene);
            }
        }

        /// <summary> シーンをアンロードする </summary>
        private async UniTask UnloadScene(Scene scene)
        {
            if (scene.IsValid())
            {
                var asyncUnload = SceneManager.UnloadSceneAsync(scene);
                await asyncUnload;
            }
        }
    }
}