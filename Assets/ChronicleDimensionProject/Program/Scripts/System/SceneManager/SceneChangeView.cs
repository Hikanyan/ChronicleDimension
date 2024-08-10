using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameJamProject.SceneManager;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamProject.SceneManagement
{
    public class SceneChangeView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingUI;
        [SerializeField] private Material _fadeMaterial;
        [SerializeField] private Texture _maskTexture;
        [SerializeField, Range(0, 1)] private float _cutoutRange;
        [SerializeField] private Slider _slider;
        [SerializeField] private float _fadeDuration = 1.0f;
        [SerializeField] private Ease _fadeEase = Ease.Linear; // Easeタイプを公開

        [SerializeReference, SubclassSelector] private IFadeStrategy _fadeStrategy; // フェードを設定

        private static readonly int MaskTex = Shader.PropertyToID("_MaskTex");

        public Material FadeMaterial => _fadeMaterial;
        private void Start()
        {
            _fadeMaterial.SetTexture(MaskTex, _maskTexture);
        }

        public void SetLoadingUIActive(bool isActive)
        {
            _loadingUI.SetActive(isActive);
        }

        public async UniTask FadeOut()
        {
            if (_fadeStrategy != null)
            {
                await _fadeStrategy.FadeOut(_fadeMaterial, _fadeDuration, _cutoutRange, _fadeEase);
            }
        }

        public async UniTask FadeIn()
        {
            if (_fadeStrategy != null)
            {
                await _fadeStrategy.FadeIn(_fadeMaterial, _fadeDuration, _cutoutRange, _fadeEase);
            }
        }

        public void UpdateProgress(float progress)
        {
            _slider.value = progress;
        }
    }
}