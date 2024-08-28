using DG.Tweening;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class ScaleDownAnimationStrategy : IButtonAnimationStrategy
    {
        public void Animate(Transform buttonTransform, CanvasGroup canvasGroup, Vector3 originalScale)
        {
            buttonTransform.DOScale(originalScale * 0.8f, 0.2f).OnComplete(() =>
            {
                buttonTransform.DOScale(originalScale, 0.2f);
            });
            canvasGroup.alpha = 0.5f;
        }
    }
}