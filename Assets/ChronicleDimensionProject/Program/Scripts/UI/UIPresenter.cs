using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public class UIPresenter<T> where T : IUIView
    {
        protected readonly T view;

        public UIPresenter(T view)
        {
            this.view = view;
        }

        public virtual UniTask Initialize()
        {
            // 初期化処理
            return UniTask.CompletedTask;
        }
    }
}