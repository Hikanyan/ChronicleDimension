using UnityEngine;
using DG.Tweening;
using UniRx;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 アニメーションをインターフェースで実装すると
 */

[RequireComponent(typeof(CanvasGroup))]
public class InputUIButton : InputUIButtonBase
{
    [SerializeField] bool _isAnimation = false;
    private CanvasGroup _button;
    private Vector3 _originalScale;

    // イベントの定義
    public event Action OnButtonDown;

    public event Action OnButtonUp;

    // 自作のクリックイベントを定義
    [Serializable]
    public class ButtonClickEvent : UnityEvent<InputUIButton>
    {
    }

    public Button.ButtonClickedEvent onClick;


    private void Start()
    {
        _button = GetComponent<CanvasGroup>();
        _originalScale = transform.localScale;
    }

    protected override void OnPointerDownEvent()
    {
        if (_isAnimation)
        {
            // DOTweenを使ってスケールを小さくするアニメーションを実行
            transform.DOScale(_originalScale * 0.8f, 0.2f);
            _button.alpha = 0.5f;
        }

        // イベントの発火
        OnButtonDown?.Invoke();
    }

    protected override void OnPointerUpEvent()
    {
        if (_isAnimation)
        {
            // DOTweenを使ってスケールを元に戻すアニメーションを実行
            transform.DOScale(_originalScale, 0.2f);
            _button.alpha = 1f;
        }

        // イベントの発火
        OnButtonUp?.Invoke();
        onClick.Invoke();
    }
}