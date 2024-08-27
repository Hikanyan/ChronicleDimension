using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI.Sample
{
    public class SampleTopScreen : MonoBehaviour
    {
        [SerializeField] private ScreenCommonRoot _screenCommonRoot;
        [SerializeField] private SampleTopScreenView _sampleTopScreenView;
        [SerializeField] private SampleTopScreenPresenter _sampleTopScreenPresenter;

        private async void Start()
        {
            if (_screenCommonRoot == null || _sampleTopScreenView == null || _sampleTopScreenPresenter == null)
            {
                Debug.LogError("One or more components are not assigned in the inspector.");
                return;
            }

            UIManager.Instance.RegisterNode(_sampleTopScreenView);

            Debug.Log("Registered Nodes:");
            // foreach (var nodeType in UIManager.Instance.GetRegisteredNodes())
            // {
            //     Debug.Log(nodeType);
            // }

            _sampleTopScreenPresenter.Initialize();

            // ノードのオープン
            await UIManager.Instance.OpenNode<SampleTopScreenView>();
        }

        private void OnDestroy()
        {
            _sampleTopScreenPresenter.Dispose();
        }
    }
}