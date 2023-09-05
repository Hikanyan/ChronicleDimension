using System;
using TMPro;
using UnityEngine;

public class Title : UIBase
{
    [SerializeField] private GameObject titleUI;
    [SerializeField] private CRIAudioList bgmName;

    private void Start()
    {
        titleUI.SetActive(false);
    }

    // Titleの実装
    void OnEnable()
    {
        //ログイン、BGM、TitleUIの表示
        titleUI.SetActive(true);
        CriAudioManager.Instance.CribgmPlay(bgmName);
    }
    
    private void Update()
    {
        
    }
}