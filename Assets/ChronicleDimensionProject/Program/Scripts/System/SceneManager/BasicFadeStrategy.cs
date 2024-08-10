using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameJamProject.SceneManager;
using UnityEngine;

namespace GameJamProject.SceneManagement
{
    [Serializable]
    public class BasicFadeStrategy : IFadeStrategy
    {
        private static readonly int Range1 = Shader.PropertyToID("_Range");

        public async UniTask FadeOut(Material fadeMaterial, float fadeDuration, float cutoutRange, Ease ease)
        {
            DOTween.To(() => cutoutRange, x => cutoutRange = x, 1, fadeDuration)
                .OnUpdate(() => fadeMaterial.SetFloat(Range1, 1 - cutoutRange))
                .SetEase(ease);
            await UniTask.Delay(TimeSpan.FromSeconds(fadeDuration));
        }

        public async UniTask FadeIn(Material fadeMaterial, float fadeDuration, float cutoutRange, Ease ease)
        {
            DOTween.To(() => cutoutRange, x => cutoutRange = x, 0, fadeDuration)
                .OnUpdate(() => fadeMaterial.SetFloat(Range1, 1 - cutoutRange))
                .SetEase(ease);
            await UniTask.Delay(TimeSpan.FromSeconds(fadeDuration));
        }
    }
}