using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public interface IUIView
    {
        UniTask Show();
        UniTask Hide();
    }
}