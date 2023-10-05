using System;
using TMPro;
using UnityEngine;

public class Title : UIBase
{
    [SerializeField] private GameObject titleUI;
    [SerializeField] private CRIAudioList bgmName;

    private Action _start;

    // Titleの実装
    void TitleStart()
    {
        //ログイン、BGM、TitleUIの表示
        CRIAudioManager.Instance.CribgmPlay(bgmName);

        // GameManagerのOnGameStartイベントにイベントハンドラを登録
        GameManager.Instance.OnGameStart += HandleGameStart;
    }

    void HandleGameStart()
    {
        // ゲームが開始されたときの処理をここに記述
        // 例えば、タイトルUIの非表示化など
        titleUI.SetActive(false);
    }
}