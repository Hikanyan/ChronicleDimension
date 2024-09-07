using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChargeCatProject.System
{
    public class ExampleFade : MonoBehaviour
    {
        [SerializeReference, SubclassSelector, Tooltip("FadeStrategyを設定して下さい")]
        private IFadeStrategy _fadeStrategy;

        private FadeManager _fadeManager;

        private void Start()
        {
            StartFadeExample().Forget();
        }

        private async UniTaskVoid StartFadeExample()
        {
            Debug.Log("FadeOut");
            await _fadeManager.FadeOut(_fadeStrategy);
            await UniTask.Delay(1000);
            Debug.Log("FadeIn");
            await _fadeManager.FadeIn(_fadeStrategy);
        }
    }
}