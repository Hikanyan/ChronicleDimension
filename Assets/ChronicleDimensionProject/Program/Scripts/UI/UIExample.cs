using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class UIExample : MonoBehaviour
    {
        public SampleStaticUIView sampleStaticView;
        public SampleAnimatedUIView sampleAnimatedView;
        private UIManager uiManager;

        private async void Start()
        {
            uiManager = new UIManager();
            uiManager.RegisterView(sampleStaticView);
            uiManager.RegisterView(sampleAnimatedView);

            await uiManager.Show<SampleStaticUIView>();
            await UniTask.Delay(2000); // 2秒後に隠す
            await uiManager.Hide<SampleStaticUIView>();

            await uiManager.Show<SampleAnimatedUIView>();
            await UniTask.Delay(2000); // 2秒後に隠す
            await uiManager.Hide<SampleAnimatedUIView>();
        }
    }
}