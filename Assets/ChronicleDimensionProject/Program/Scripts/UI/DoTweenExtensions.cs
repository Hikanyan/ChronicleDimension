using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace ChronicleDimensionProject.UI
{
    /// <summary>
    /// DoTweenのタスクをUniTaskとして扱うために、カスタム拡張メソッド
    /// </summary>
    public static class DoTweenExtensions
    {
        /// <summary>
        /// DoTweenのTweenをUniTaskに変換する
        /// </summary>
        /// <param name="tween"></param>
        /// <returns></returns>
        public static UniTask ToUniTask(this Tween tween)
        {
            var tcs = new UniTaskCompletionSource();

            tween.OnComplete(() => tcs.TrySetResult())
                .OnKill(() => tcs.TrySetCanceled())
                .OnRewind(() => tcs.TrySetResult())
                .OnPause(() => tcs.TrySetCanceled());

            return tcs.Task;
        }
    }
}