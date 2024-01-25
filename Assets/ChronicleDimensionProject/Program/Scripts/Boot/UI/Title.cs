using System;
using ChronicleDimension.Core;
using ChronicleDimensionProject.Scripts.Core.UI;
using TMPro;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class Title : UIBase, IUserInterface
{
    [SerializeField] private GameObject titleUI;
    [SerializeField] private string bgmName;

    private Action _start;

    public ReactiveProperty<bool> IsVisible { get; }
    public void Show()
    {
        CriAudioManager.Instance.PlayBGM(CriAudioManager.CueSheet.Bgm,bgmName);
        IsVisible.Value = true;
    }

    public void Hide()
    {
        CriAudioManager.Instance.StopBGM();
        IsVisible.Value = false;
    }
}