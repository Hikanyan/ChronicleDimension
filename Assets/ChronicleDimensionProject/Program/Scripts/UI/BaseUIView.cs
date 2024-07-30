using Cysharp.Threading.Tasks;
using UnityEngine;


namespace ChronicleDimensionProject.UI
{
    public class BaseUIView : MonoBehaviour, IUIView
    {
        public virtual async UniTask Show()
        {
            gameObject.SetActive(true);
            await DoTweenAnimations.ShowAnimation(transform); // DoTweenによるアニメーション
        }

        public virtual async UniTask Hide()
        {
            await DoTweenAnimations.HideAnimation(transform); // DoTweenによるアニメーション
            gameObject.SetActive(false);
        }
    }
}