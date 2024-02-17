using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace ChronicleDimensionProject.Common.UI
{
    public class LoadScenePresenter
    {
        private readonly LoadingSceneView _view;
        private readonly LoadSceneModel _model;
        private readonly float _minimumLoadTime;

        public LoadScenePresenter(LoadingSceneView view, float minimumLoadTime)
        {
            _view = view;
            _model = new LoadSceneModel();
            _minimumLoadTime = minimumLoadTime;
            _model.Progress.Subscribe(progress => _view.UpdateProgress(progress)).AddTo(_view);
        }

        public async UniTask LoadNextScene(string sceneName)
        {
            await _view.FadeOut();
            _view.ShowLoading(true);

            var startTime = Time.time;
            var displayProgress = 0f;

            await _model.LoadSceneAsync(sceneName);

            // 最小ロード時間とシーンが準備完了になるまで待機
            await UniTask.WhenAll(
                UniTask.Delay(TimeSpan.FromSeconds(_minimumLoadTime)),
                UniTask.WaitUntil(() => _model.IsSceneReady.Value)
            );
            
            _view.ShowLoading(false);
            await _view.FadeIn();
        }
    }
}