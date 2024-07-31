using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public class SampleUIPresenter
    {
        private readonly SampleAnimatedUIView _view;
        private readonly SampleUIModel _model;

        public SampleUIPresenter(SampleAnimatedUIView view, SampleUIModel model)
        {
            this._view = view;
            this._model = model;
        }

        public async UniTask Initialize()
        {
            // 初期化ロジック
            UpdateView();
            await _view.Show();
        }

        public void UpdateModel(string title, int count)
        {
            _model.Title = title;
            _model.Count = count;
            UpdateView();
        }

        private void UpdateView()
        {
            _view.SetTitle(_model.Title);
            _view.SetCount(_model.Count);
        }

        public async UniTask Show()
        {
            UpdateView();
            await _view.Show();
        }

        public async UniTask Hide()
        {
            await _view.Hide();
        }
    }

}