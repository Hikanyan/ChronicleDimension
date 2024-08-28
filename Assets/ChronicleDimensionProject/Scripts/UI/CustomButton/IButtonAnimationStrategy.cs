using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public interface IButtonAnimationStrategy
    {
        void Animate(Transform buttonTransform, CanvasGroup canvasGroup, Vector3 originalScale);
    }
}