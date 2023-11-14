using UnityEngine;
using DG.Tweening;
using UniRx;
using System;


[RequireComponent(typeof(CanvasGroup))]
public class InputUIButton : InputUIButtonBase
{
    private CanvasGroup _button;
    private Vector3 _originalScale;

    private GameObject _target;

    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }

    // イベントの定義
    public event Action OnButtonDown;
    public event Action OnButtonUp;

    private void Start()
    {
        _button = GetComponent<CanvasGroup>();
        _originalScale = transform.localScale;
    }

    protected override void OnPointerDownEvent()
    {
        // DOTweenを使ってスケールを小さくするアニメーションを実行
        transform.DOScale(_originalScale * 0.8f, 0.2f);
        _button.alpha = 0.5f;

        // イベントの発火
        OnButtonDown?.Invoke();
    }

    protected override void OnPointerUpEvent()
    {
        // DOTweenを使ってスケールを元に戻すアニメーションを実行
        transform.DOScale(_originalScale, 0.2f);
        _button.alpha = 1f;
        ToggleVisibility(); // 表示・非表示を切り替える
        // イベントの発火
        OnButtonUp?.Invoke();
    }

    public void ToggleVisibility()
    {
        _target.SetActive(!_target.activeSelf);
    }
}