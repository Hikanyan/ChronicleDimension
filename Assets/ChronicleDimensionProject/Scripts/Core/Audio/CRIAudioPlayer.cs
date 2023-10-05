using System;
using System.Collections.Generic;
using CriWare;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class CRIAudioPlayer : MonoBehaviour
{
    [SerializeField] string streamingAssetsPathAcf;
    [SerializeField] string _cueSheetBGM = "CueSheet_Chronicle_Dimention_20221024_2"; //.acb
    [SerializeField] string _cueSheetSE = "CueSheet_SE"; //.acb

    private void Start()
    {
        // acf設定
        string path = Common.streamingAssetsPath + $"/{streamingAssetsPathAcf}.acf";
        CriAtomEx.RegisterAcf(null, path);
        // CriAtom作成
        new GameObject().AddComponent<CriAtom>();
        // BGM acb追加
        CriAtom.AddCueSheet(_cueSheetBGM, $"{_cueSheetBGM}.acb", null, null);
        // SE acb追加
        CriAtom.AddCueSheet(_cueSheetSE, $"{_cueSheetSE}.acb", null, null);

        CriAudioManager.Instance.PlayBGM(_cueSheetBGM, "BGM_Aries_20221020_BPM180");
    }
}