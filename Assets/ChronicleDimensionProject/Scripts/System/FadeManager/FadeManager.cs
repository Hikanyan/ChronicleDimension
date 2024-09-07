using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace ChronicleDimensionProject.System
{
    public class FadeManager
    {
        private Material _material;
        private IFadeStrategy _defaultFadeStrategy;

        // VContainerでデフォルトのFadeStrategyを注入
        [Inject]
        public FadeManager(IFadeStrategy defaultFadeStrategy)
        {
            _defaultFadeStrategy = defaultFadeStrategy;
        }

        public void SetFadeMaterial(Material material)
        {
            _material = material;
        }

        // FadeOut: 動的にIFadeStrategyを渡すか、デフォルトを使う
        public async UniTask FadeOut(IFadeStrategy fadeStrategy = null)
        {
            IFadeStrategy strategy = fadeStrategy ?? _defaultFadeStrategy;
            if (_material != null && strategy != null)
            {
                await strategy.FadeOut(_material);
            }
        }

        // FadeIn: 動的にIFadeStrategyを渡すか、デフォルトを使う
        public async UniTask FadeIn(IFadeStrategy fadeStrategy = null)
        {
            IFadeStrategy strategy = fadeStrategy ?? _defaultFadeStrategy;
            if (_material != null && strategy != null)
            {
                await strategy.FadeIn(_material);
            }
        }
    }
}