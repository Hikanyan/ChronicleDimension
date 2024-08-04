using UnityEngine;
using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public class TitleUIView : SceneNode
    {
        [SerializeField] private WindowNode _mainWindow;
        [SerializeField] private Background _background;
        [SerializeField] private TitleMenu _titleMenu;

        private async void Start()
        {
            // ウィンドウノードの初期化と登録
            _mainWindow.AddScreen(_background);
            _mainWindow.AddScreen(_titleMenu);

            AddWindow(_mainWindow);

            UIManager.Instance.RegisterNode(this);
            UIManager.Instance.RegisterNode(_mainWindow);
            UIManager.Instance.RegisterNode(_background);
            UIManager.Instance.RegisterNode(_titleMenu);

            Debug.Log("Registered Nodes:");
            foreach (var nodeType in UIManager.Instance.GetRegisteredNodes())
            {
                Debug.Log(nodeType);
            }

            // シーンノードの初期化
            await OnInitialize();

            // ノードのオープン
            await UIManager.Instance.OpenNode<WindowNode>();
        }
    }
}