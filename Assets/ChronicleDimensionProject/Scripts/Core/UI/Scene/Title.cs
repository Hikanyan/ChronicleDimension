using System;
using TMPro;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class Title : UIBase
{
    [SerializeField] private GameObject titleUI;
    [SerializeField] private string bgmName;

    private Action _start;

    // Titleの実装
    void Start()
    {
        //ログイン、BGM、TitleUIの表示

        titleUI.SetActive(true);
        //CriAudioManager.Instance.PlayBGM(CriAudioManager.Instance.CurrentCueSheet.Bgm);
    }
}