using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public interface IAnimatedUIView : IUIView
    {
        UniTask AnimateShow();
        UniTask AnimateHide();
    }

}