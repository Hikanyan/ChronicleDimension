using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public class MenuUIButton : InputUIButton
{
    CanvasGroup _button;
    private Vector3 _originalScale;
    private void Start()
    {
        _button = GetComponent<CanvasGroup>();
        _originalScale = transform.localScale;
    }

    protected override void OnPointerDown()
    {
        //transform.DO
        Debug.Log("OnPointerDown");
        transform.DOScale(_originalScale * 0.8f, 0.2f);
    }

    protected override void OnPointerUp()
    {
        Debug.Log("OnPointerUp");
        transform.DOScale(_originalScale, 0.2f);
    }
}