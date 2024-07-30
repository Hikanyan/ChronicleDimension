using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public class SampleUIPresenter : UIPresenter<SampleUIView>
    {
        public SampleUIPresenter(SampleUIView view) : base(view)
        {
        }

        public override async UniTask Initialize()
        {
            await base.Initialize();
            // 特定の初期化処理
        }
    }
}