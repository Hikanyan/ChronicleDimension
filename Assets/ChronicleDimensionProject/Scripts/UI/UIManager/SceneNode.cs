using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.UI
{
    public class SceneNode : Node
    {
        private List<WindowNode> windows = new List<WindowNode>();

        public void AddWindow(WindowNode window)
        {
            windows.Add(window);
        }

        public void RemoveWindow(WindowNode window)
        {
            windows.Remove(window);
        }

        public override async UniTask OnInitialize()
        {
            foreach (var window in windows)
            {
                await window.OnInitialize();
            }
        }
    }

    public class WindowNode : Node
    {
        private List<ScreenNode> screens = new List<ScreenNode>();

        public void AddScreen(ScreenNode screen)
        {
            screens.Add(screen);
        }

        public void RemoveScreen(ScreenNode screen)
        {
            screens.Remove(screen);
        }

        public override async UniTask OnInitialize()
        {
            foreach (var screen in screens)
            {
                await screen.OnInitialize();
            }
        }
    }

    public class ScreenNode : Node
    {
        public override async UniTask OnInitialize()
        {
            // Initialize screen-specific logic here
            await base.OnInitialize();
        }

        public async UniTask DisplayContent()
        {
            // Display content-specific logic here
        }
    }
}