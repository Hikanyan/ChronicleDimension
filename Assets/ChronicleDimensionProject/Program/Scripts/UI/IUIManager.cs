using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public interface IUIManager
    {
        UniTask Show<T>() where T : IUIView;
        UniTask Hide<T>() where T : IUIView;
    }
}