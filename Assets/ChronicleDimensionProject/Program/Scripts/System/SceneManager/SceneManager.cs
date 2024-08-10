using System.Collections.Generic;
using ChronicleDimensionProject.System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJamProject.SceneManagement
{
    public class SceneManager : Singleton<SceneManager>
    {
        protected override bool UseDontDestroyOnLoad => true;

        private Scene _lastScene;
        private SceneLoader _sceneLoader;
        private readonly string _neverUnloadSceneName = "ManagerScene";
        private IFadeStrategy _fadeStrategy;
        private readonly Stack<string> _sceneHistory = new Stack<string>();

        protected override async void OnAwake()
        {
            base.OnAwake(); // SingletonのAwakeメソッドを呼び出す
            // 初期化処理
            _sceneLoader = new SceneLoader();
            _fadeStrategy = new BasicFadeStrategy(); // インターフェースを実装した具体的なインスタンスを設定

            // 現在のアクティブなシーンを取得して_lastSceneに設定
            _lastScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            // ManagerSceneを必ずロードする
            await LoadNeverUnloadSceneAsync();
        }

        private async UniTask LoadNeverUnloadSceneAsync()
        {
            await _sceneLoader.LoadSceneAsync(_neverUnloadSceneName, LoadSceneMode.Additive);
        }

        public async UniTask LoadSceneWithFade(string sceneName, Material fadeMaterial, float fadeDuration,
            float cutoutRange, Ease ease, bool recordHistory = true)
        {
            SceneChangeView sceneChangeView = FindObjectOfType<SceneChangeView>();
            if (sceneChangeView != null)
            {
                sceneChangeView.SetLoadingUIActive(true);
                await sceneChangeView.FadeOut();
            }

            await _fadeStrategy.FadeOut(fadeMaterial, fadeDuration, cutoutRange, ease);
            await LoadSceneWithProgress(sceneName, fadeMaterial, fadeDuration, cutoutRange, ease, sceneChangeView);

            if (recordHistory && !_sceneLoader.IsSceneLoaded(sceneName))
            {
                _sceneHistory.Push(sceneName);
            }

            await _fadeStrategy.FadeIn(fadeMaterial, fadeDuration, cutoutRange, ease);
            if (sceneChangeView != null)
            {
                await sceneChangeView.FadeIn();
                sceneChangeView.SetLoadingUIActive(false);
            }

            if (_lastScene.isLoaded)
            {
                await UnloadSceneWithFade(_lastScene.name, fadeMaterial, fadeDuration, cutoutRange, ease);
            }

            _lastScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
        }

        public async UniTask UnloadSceneWithFade(string sceneName, Material fadeMaterial, float fadeDuration,
            float cutoutRange, Ease ease)
        {
            if (sceneName == _neverUnloadSceneName)
            {
                Debug.LogWarning($"Cannot unload the never unload scene: {sceneName}");
                return;
            }

            SceneChangeView sceneChangeView = FindObjectOfType<SceneChangeView>();
            if (sceneChangeView != null)
            {
                sceneChangeView.SetLoadingUIActive(true);
                await sceneChangeView.FadeOut();
            }

            await _fadeStrategy.FadeOut(fadeMaterial, fadeDuration, cutoutRange, ease);
            await UnloadSceneWithProgress(sceneName, fadeMaterial, fadeDuration, cutoutRange, ease, sceneChangeView);
            await _fadeStrategy.FadeIn(fadeMaterial, fadeDuration, cutoutRange, ease);

            if (sceneChangeView != null)
            {
                await sceneChangeView.FadeIn();
                sceneChangeView.SetLoadingUIActive(false);
            }
        }

        private async UniTask LoadSceneWithProgress(string sceneName, Material fadeMaterial, float fadeDuration,
            float cutoutRange, Ease ease, SceneChangeView sceneChangeView)
        {
            var loadSceneOperation =
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!loadSceneOperation.isDone)
            {
                if (sceneChangeView != null)
                {
                    sceneChangeView.UpdateProgress(loadSceneOperation.progress);
                }

                await UniTask.Yield();
            }
        }

        private async UniTask UnloadSceneWithProgress(string sceneName, Material fadeMaterial, float fadeDuration,
            float cutoutRange, Ease ease, SceneChangeView sceneChangeView)
        {
            var unloadSceneOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            while (!unloadSceneOperation.isDone)
            {
                if (sceneChangeView != null)
                {
                    sceneChangeView.UpdateProgress(unloadSceneOperation.progress);
                }

                await UniTask.Yield();
            }
        }

        public async UniTask ReloadSceneWithFade(Material fadeMaterial, float fadeDuration, float cutoutRange,
            Ease ease)
        {
            if (_sceneHistory.Count > 0)
            {
                string currentScene = _sceneHistory.Pop();
                await UnloadSceneWithFade(currentScene, fadeMaterial, fadeDuration, cutoutRange, ease);
                await LoadSceneWithFade(currentScene, fadeMaterial, fadeDuration, cutoutRange, ease, false);
            }
        }

        public async UniTask LoadPreviousSceneWithFade(Material fadeMaterial, float fadeDuration, float cutoutRange,
            Ease ease)
        {
            if (_sceneHistory.Count > 1)
            {
                string currentScene = _sceneHistory.Pop();
                string previousScene = _sceneHistory.Peek();
                await UnloadSceneWithFade(currentScene, fadeMaterial, fadeDuration, cutoutRange, ease);
                await LoadSceneWithFade(previousScene, fadeMaterial, fadeDuration, cutoutRange, ease, false);
            }
        }

        public async UniTask LoadScene(string sceneName, bool recordHistory = true)
        {
            await _sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (recordHistory && !_sceneLoader.IsSceneLoaded(sceneName))
            {
                _sceneHistory.Push(sceneName);
            }

            // 直前のシーンをアンロード
            if (_lastScene.isLoaded)
            {
                await UnloadScene(_lastScene.name);
            }

            _lastScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
        }

        public async UniTask UnloadScene(string sceneName)
        {
            if (sceneName == _neverUnloadSceneName)
            {
                Debug.LogWarning($"Cannot unload the never unload scene: {sceneName}");
                return;
            }

            await _sceneLoader.UnloadSceneAsync(sceneName);
        }
    }
}