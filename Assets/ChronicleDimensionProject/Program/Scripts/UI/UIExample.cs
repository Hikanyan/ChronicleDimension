using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class UIExample : MonoBehaviour
    {
        public SampleStaticUIView sampleStaticView;
        public SampleAnimatedUIView sampleAnimatedView;

        private async void Start()
        {
            // モデルの作成
            var sampleModel = new SampleUIModel("Initial Title", 0);

            // UIManagerにビューとプレゼンターを登録
            UIManager.Instance.RegisterView(sampleStaticView);
            UIManager.Instance.RegisterView(sampleAnimatedView);
            UIManager.Instance.RegisterPresenter<SampleUIPresenter, SampleAnimatedUIView>(sampleAnimatedView,
                sampleModel);

            // Presenterの初期化
            var presenter = (SampleUIPresenter)UIManager.Instance.GetPresenter<SampleUIPresenter>();
            await presenter.Initialize();

            // モデルデータの更新
            presenter.UpdateModel("Updated Title", 10);

            // Presenterを通してUIを表示
            await UIManager.Instance.ShowPresenter<SampleUIPresenter>();
            await UniTask.Delay(2000); // 2秒後に隠す
            await UIManager.Instance.HidePresenter<SampleUIPresenter>();
        }
    }
}