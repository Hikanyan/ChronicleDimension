using DG.Tweening;
using UnityEngine;
using VContainer;
using Cysharp.Threading.Tasks;

namespace ChronicleDimensionProject.Common
{
    public class FadeView : MonoBehaviour
    {
        [SerializeField, Tooltip("フェード用のPanelを設定")]
        private GameObject _fadePanel;

        [SerializeField, Tooltip("フェード用のImageを設定")]
        private Material _fadeMaterial;

        [SerializeField, Tooltip("Fade用テクスチャ")]
        private Texture _maskTexture;

        private static readonly int MaskTex = Shader.PropertyToID("_MaskTex");

        private FadeManager _fadeManager;

        [Inject]
        public void Construct(FadeManager fadeManager)
        {
            _fadeManager = fadeManager;
        }

        private void Awake()
        {
            if (_fadeMaterial == null) return;

            _fadeManager.SetFadeMaterial(_fadeMaterial);

            if (_maskTexture != null)
            {
                _fadeMaterial.SetTexture(MaskTex, _maskTexture);
            }
        }

        public async UniTask FadeOut()
        {
            await _fadeManager.FadeOut();
        }

        public async UniTask FadeIn()
        {
            await _fadeManager.FadeIn();
        }
    }
}