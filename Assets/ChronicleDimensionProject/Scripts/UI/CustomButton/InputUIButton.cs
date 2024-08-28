using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ChronicleDimensionProject.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class InputUIButton : Button
    {
        [SerializeField] private bool _isAnimation = false;
        [SerializeReference, SubclassSelector] private IButtonAnimationStrategy _animationStrategy;
        private CanvasGroup _canvasGroup;
        private Vector3 _originalScale;


        // 自作のクリックイベント
        [Serializable]
        public class ButtonClickEvent : UnityEvent<InputUIButton>
        {
        }

        public ButtonClickEvent _onCustomClick;

        protected override void Start()
        {
            base.Start();
            _canvasGroup = GetComponent<CanvasGroup>();
            _originalScale = transform.localScale;

            // ボタンクリックのリスナーを追加
            onClick.AddListener(OnButtonClick);
        }

        protected override void OnDestroy()
        {
            // リスナーの解除
            onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (_isAnimation && _animationStrategy != null)
            {
                _animationStrategy.Animate(transform, _canvasGroup, _originalScale);
            }

            // カスタムイベントを発火
            _onCustomClick?.Invoke(this);
        }

        public void SetAnimationStrategy(IButtonAnimationStrategy strategy)
        {
            _animationStrategy = strategy;
        }

        public void SetAnimation(bool enable)
        {
            _isAnimation = enable;
        }
    }
}