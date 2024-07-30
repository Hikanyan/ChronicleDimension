using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class AnimatedUIView : MonoBehaviour, IAnimatedUIView
    {
        public virtual async UniTask Show()
        {
            gameObject.SetActive(true);
            await AnimateShow();
        }

        public virtual async UniTask Hide()
        {
            await AnimateHide();
            gameObject.SetActive(false);
        }

        public virtual UniTask AnimateShow()
        {
            return DoTweenAnimations.ShowAnimation(transform);
        }

        public virtual UniTask AnimateHide()
        {
            return DoTweenAnimations.HideAnimation(transform);
        }
    }
}