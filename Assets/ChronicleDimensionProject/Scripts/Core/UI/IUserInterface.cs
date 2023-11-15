using UniRx;

namespace ChronicleDimensionProject.Scripts.Core.UI
{
    public interface IUserInterface
    {
        //UIの表示状態を管理するReactiveProperty
        ReactiveProperty<bool> IsVisible { get; }
        void Show();
        void Hide();
    }
}