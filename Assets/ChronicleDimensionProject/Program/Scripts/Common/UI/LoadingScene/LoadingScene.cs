using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.Common.UI
{
    public class LoadingScene : MonoBehaviour
    {
        [Tooltip("ロード中に表示するUI")] [SerializeField]
        private GameObject loadingUI;

        [Tooltip("フェードに使用するマテリアル")] [SerializeField]
        private Material fadeMaterial;

        [Tooltip("フェードに使用するマスクテクスチャ")] [SerializeField]
        private Texture maskTexture;

        [Tooltip("フェードの範囲")] [SerializeField, Range(0, 1)]
        private float cutoutRange;

        [Tooltip("プログレスバー")] [SerializeField] private Slider slider;
        [Tooltip("フェード時間")] [SerializeField] private float fadeDuration = 1.0f;
        [Tooltip("最低ロード時間")] [SerializeField] private float minimumLoadTime = 2.0f;

        public static LoadingScene Instance { get; private set; }
        private AsyncOperation _async;
        private Action _onComplete;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary> 次のシーンをロードする </summary>
        public async UniTask LoadNextScene(string sceneName)
        {
            await FadeOut();

            loadingUI.SetActive(true);
            var startTime = Time.time;
            var targetProgress = 0f;
            var displayProgress = 0f;

            _async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            while (Time.time - startTime < minimumLoadTime || _async is { isDone: false })
            {
                if (_async != null)
                {
                    targetProgress = Mathf.Clamp01(_async.progress);
                }

                // プログレスバーを滑らかに進める
                displayProgress = Mathf.MoveTowards(
                    displayProgress, targetProgress, Time.deltaTime / minimumLoadTime);
                slider.value = displayProgress;

                await UniTask.DelayFrame(1);
            }

            loadingUI.SetActive(false);
            _onComplete?.Invoke();

            await FadeIn();
        }

        /// <summary> ロード完了時に実行する処理を登録する </summary>
        private async UniTask FadeOut()
        {
            fadeMaterial.SetTexture("_MaskTex", maskTexture);

            // DOTweenを使用してRangeパラメータをアニメーション
            DOTween.To(() => cutoutRange, x => cutoutRange = x, 1, fadeDuration)
                .OnUpdate(() => fadeMaterial.SetFloat("_Range", 1 - cutoutRange))
                .SetEase(Ease.Linear);

            // フェードアウトが終わるまで待機する
            await UniTask.Delay(TimeSpan.FromSeconds(fadeDuration));
        }

        private async UniTask FadeIn()
        {
            // DOTweenアニメーションを待機する
            DOTween.To(() => cutoutRange, x => cutoutRange = x, 0, fadeDuration)
                .OnUpdate(() => fadeMaterial.SetFloat("_Range", 1 - cutoutRange))
                .SetEase(Ease.Linear);
            await UniTask.Delay(TimeSpan.FromSeconds(fadeDuration));
        }
    }
}