using System;
using Cysharp.Threading.Tasks;
using UniRx;

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
            // LoadSceneModelのインスタンス化が必要な場合はここで行う
            _model = new LoadSceneModel(); // もしコンストラクタで必要な引数があれば、ここで渡す
            _minimumLoadTime = minimumLoadTime;

            // モデルの進捗プロパティを購読して、ビューの進捗を更新する
            _model.Progress.Subscribe(progress =>
            {
                // ビューの進捗更新メソッドを呼び出す
                _view.UpdateProgress(progress);
            }).AddTo(_view);

            // モデルのシーン準備完了プロパティを購読して、準備完了時の処理を行う
            _model.IsSceneReady.Where(isReady => isReady).Subscribe(_ =>
            {
                // シーン準備完了時のビュー更新処理をここに記述
                _view.ShowLoading(false);
                _view.FadeIn().Forget(); // FadeInは非同期処理のため、Forgetを呼び出して待機しない
            }).AddTo(_view);
        }

        public async UniTask LoadNextScene(string sceneName)
        {
            // ローディング開始前のフェードアウトとローディング表示
            await _view.FadeOut();
            _view.ShowLoading(true);

            // モデルを通じてシーンロードを開始
            await _model.LoadSceneAsync(sceneName);

            // 最小ロード時間が経過するまで待機
            await UniTask.Delay(TimeSpan.FromSeconds(_minimumLoadTime));
        }
    }
}