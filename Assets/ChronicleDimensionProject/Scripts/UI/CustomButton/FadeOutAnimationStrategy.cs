using DG.Tweening;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class FadeOutAnimationStrategy : IButtonAnimationStrategy
    {
        public void Animate(Transform buttonTransform, CanvasGroup canvasGroup, Vector3 originalScale)
        {
            buttonTransform.DOScale(originalScale * 0.9f, 0.2f).OnComplete(() =>
            {
                buttonTransform.DOScale(originalScale, 0.2f);
            });
            canvasGroup.DOFade(0.5f, 0.2f).OnComplete(() => { canvasGroup.DOFade(1f, 0.2f); });
        }
    }
}