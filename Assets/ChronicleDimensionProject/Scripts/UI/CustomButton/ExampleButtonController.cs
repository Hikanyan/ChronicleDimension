using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class ExampleButtonController : MonoBehaviour
    {
        private InputUIButton _inputUIButton;

        private void Start()
        {
            _inputUIButton = GetComponent<InputUIButton>();

            // アニメーションストラテジーを設定
            _inputUIButton.SetAnimationStrategy(new ScaleDownAnimationStrategy());

            // アニメーションを有効化
            _inputUIButton.SetAnimation(true);

            // カスタムイベントのリスナーを設定
            _inputUIButton._onCustomClick.AddListener(OnCustomButtonClick);
        }

        private void OnCustomButtonClick(InputUIButton button)
        {
            Debug.Log("Custom Button Clicked!");
            // ここにクリック時に行いたい処理を追加します
        }
    }
}