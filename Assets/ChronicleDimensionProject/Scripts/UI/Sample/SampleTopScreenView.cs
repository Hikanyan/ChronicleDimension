using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.UI.Sample
{
    public class SampleTopScreenView : ScreenNode
    {
        [SerializeField] private Button someButton;

        private void Start()
        {
            if (someButton == null)
            {
                Debug.LogError("SomeButton is not assigned.");
                return;
            }

            someButton.onClick.AddListener(OnSomeButtonClicked);
        }

        private void OnSomeButtonClicked()
        {
            // ボタンがクリックされた時の処理を実装します。
        }

        public override async UniTask OnOpenIn()
        {
            gameObject.SetActive(true);
            await base.OnOpenIn();
        }

        public override async UniTask OnCloseOut()
        {
            gameObject.SetActive(false);
            await base.OnCloseOut();
        }
    }
}