using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.Common.UI
{
    public class LoadingSceneView : MonoBehaviour, ILoadSceneView
    {
        [SerializeField] private GameObject loadingUI;
        [SerializeField] private Slider progressBar;
        [SerializeField] private float duration = 1.0f;
        [SerializeField] private float minimumLoadTime = 2.0f;

        private LoadScenePresenter _presenter;

        private void Awake()
        {
            _presenter = new LoadScenePresenter(this, minimumLoadTime);
        }

        /// <summary> ローディング画面を表示するかどうかを設定する </summary>
        public void ShowLoading(bool show)
        {
            loadingUI.SetActive(show);
        }

        /// <summary> ローディング画面のプログレスバーの値を更新する
        /// </summary>
        public void UpdateProgress(float progress)
        {
            duration = 0.5f; // ここで適切なアニメーション時間を設定
            DOTween.To(() => progressBar.value, x => progressBar.value = x, progress, duration);
        }


        /// <summary> 次のシーンをロードする </summary>
        public async UniTask LoadScene(string sceneName)
        {
            await _presenter.LoadNextScene(sceneName);
        }

        public async UniTask FadeOut()
        {
            // フェードアウトの実装
        }

        public async UniTask FadeIn()
        {
            // フェードインの実装
        }
    }
}