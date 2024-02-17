using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChronicleDimensionProject.Common.UI
{
    public class LoadSceneModel
    {
        public IReadOnlyReactiveProperty<float> Progress => _progress;
        private readonly ReactiveProperty<float> _progress = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<bool> IsSceneReady => _isSceneReady;
        private readonly ReactiveProperty<bool> _isSceneReady = new ReactiveProperty<bool>();
        public async UniTask LoadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            // UniRxを使用して進捗を監視
            Observable.EveryUpdate()
                .Select(_ => asyncOperation.progress)
                .TakeWhile(progress => progress < 0.9f)
                .Subscribe(progress =>
                {
                    _progress.Value = progress / 0.9f;
                }).AddTo( SceneManager.GetActiveScene().GetRootGameObjects()[0]);
            
            await UniTask.WaitUntil(() => asyncOperation.progress >= 0.9f);
            _progress.Value = 1f;
            _isSceneReady.Value = true;
        }
    }
}