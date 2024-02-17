using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

namespace ChronicleDimensionProject.Common.UI
{
    public class LoadSceneModel
    {
        private readonly ReactiveProperty<float> _progress = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> Progress => _progress;

        private readonly ReactiveProperty<bool> _isSceneReady = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsSceneReady => _isSceneReady;

        public async UniTask LoadSceneAsync(string sceneName)
        {
            // シーンロード前に準備状態をリセット
            _isSceneReady.Value = false;

            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                _progress.Value = asyncOperation.progress;
                await UniTask.Yield();
            }

            // シーンロードが完了したら、準備完了としてマーク
            _progress.Value = 1f; // ロード完了を示す
            _isSceneReady.Value = true;
        }
    }
}