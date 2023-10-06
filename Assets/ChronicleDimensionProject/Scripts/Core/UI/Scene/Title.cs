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
        //CriAudioManager.Instance.PlayBGM(bgmName);

        // GameManagerのOnGameStartイベントにイベントハンドラを登録
        _start += HandleGameStart;
    }

    void HandleGameStart()
    {
        // ゲームが開始されたときの処理をここに記述
        // 例えば、タイトルUIの非表示化など
        titleUI.SetActive(false);
        
    }
}