using UnityEngine;

namespace ChronicleDimensionProject.UI.Sample
{
    public class SampleTopScreenPresenter : MonoBehaviour
    {
        [SerializeField] private SampleTopScreenView _sampleTopScreenView;
        [SerializeField] private ScreenCommonRoot _screenCommonRoot;

        public void Initialize()
        {
            if (_sampleTopScreenView == null || _screenCommonRoot == null)
            {
                Debug.LogError("SampleTopScreenView or ScreenCommonRoot is not assigned.");
                return;
            }
            // 初期化ロジック
        }

        public void Dispose()
        {
            // リソースの解放ロジック
        }
    }
}