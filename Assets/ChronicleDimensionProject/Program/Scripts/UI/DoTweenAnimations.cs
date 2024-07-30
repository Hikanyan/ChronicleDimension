using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public static class DoTweenAnimations
    {
        public static UniTask ShowAnimation(Transform transform)
        {
            // 任意のアニメーション処理
            return transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).ToUniTask();
        }

        public static UniTask HideAnimation(Transform transform)
        {
            // 任意のアニメーション処理
            return transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).ToUniTask();
        }
    }
}