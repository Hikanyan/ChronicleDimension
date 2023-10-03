using System;
using TMPro;
using UnityEngine;

public class Title : UIBase
{
    [SerializeField] private GameObject titleUI;
    [SerializeField] private CriAudioList bgmName;


    // Titleの実装
    void OnEnable()
    {
        //ログイン、BGM、TitleUIの表示
        //CRIAudioManager.Instance.CribgmPlay(bgmName);
    }
    
    private void Update()
    {
        
    }

}