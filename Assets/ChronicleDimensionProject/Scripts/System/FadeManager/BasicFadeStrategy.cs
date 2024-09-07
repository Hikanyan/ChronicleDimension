using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ChargeCatProject.System
{
    [Serializable]
    public class BasicFadeStrategy : IFadeStrategy
    {
        [SerializeField] private Texture _maskTexture;
        [SerializeField] private float _fadeDuration = 1.0f;
        [SerializeField] private Ease _ease = Ease.Linear;

        private static readonly int Range1 = Shader.PropertyToID("_CutOff");
        private static readonly int MaskTex = Shader.PropertyToID("_MaskTex");
        private float _cutoutRange;


        /// <summary>
        /// DOTweenのFadeを使用してフェードアウトします。
        /// </summary>
        public async UniTask FadeOut(Material material)
        {
            // マスクテクスチャが設定されている場合、それをマテリアルに適用
            if (_maskTexture != null)
            {
                material.SetTexture(MaskTex, _maskTexture);
            }

            // フェードアウト中にRangeを更新しながらアルファをフェード
            await DOTween.To(() => _cutoutRange, x => _cutoutRange = x, 1f, _fadeDuration)
                .OnUpdate(() =>
                {
                    material.SetFloat(Range1, 1 - _cutoutRange);
                    // Debug.Log($"CutoutRange{_cutoutRange}");
                })
                .SetEase(_ease)
                .ToUniTask();
        }

        /// <summary>
        /// DOTweenのFadeを使用してフェードインします。
        /// </summary>
        public async UniTask FadeIn(Material material)
        {
            // マスクテクスチャが設定されている場合、それをマテリアルに適用
            if (_maskTexture != null)
            {
                material.SetTexture(MaskTex, _maskTexture);
            }

            // フェードイン中にRangeを更新しながらアルファをフェード
            await DOTween.To(() => _cutoutRange, x => _cutoutRange = x, 0f, _fadeDuration)
                .OnUpdate(() => material.SetFloat(Range1, 1 - _cutoutRange))
                .SetEase(_ease)
                .ToUniTask();
        }
    }
}