using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
using UniRx;
using Cysharp.Threading.Tasks;


public class CRIAudioManager : AbstractSingleton<CRIAudioManager>
{
    string _streamingAssetsPathAcf = "ChronicleDimension";
    string _cueSheetBGM = "CueSheet_BGM";
    string _cueSheetSe = "CueSheet_SE";
    
    CriAtomSource _criAtomSourceBgm;
    CriAtomSource _criAtomSourceSe;
    
    private CriAtomExPlayback _criAtomExPlaybackBGM;
    CriAtomEx.CueInfo _cueInfo;

    protected override void OnAwake()
    {
        //acf設定
        string path = Common.streamingAssetsPath + $"/{_streamingAssetsPathAcf}.acf";

        CriAtomEx.RegisterAcf(null, path);

        // CriAtom作成
        new GameObject().AddComponent<CriAtom>();

        // BGM acb追加
        CriAtom.AddCueSheet(_cueSheetBGM, $"{_cueSheetBGM}.acb", null, null);
        // SE acb追加
        CriAtom.AddCueSheet(_cueSheetSe, $"{_cueSheetSe}.acb", null, null);


        //BGM用のCriAtomSourceを作成
        _criAtomSourceBgm = gameObject.AddComponent<CriAtomSource>();
        _criAtomSourceBgm.cueSheet = _cueSheetBGM;
        //SE用のCriAtomSourceを作成
        _criAtomSourceSe = gameObject.AddComponent<CriAtomSource>();
        _criAtomSourceSe.cueSheet = _cueSheetSe;
    }

    
    public async UniTask CribgmPlay(CRIAudioList index, float delayTime)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delayTime));
        CribgmPlay(index);
    }
    


    public void CribgmPlay(CRIAudioList index)
    {
        string bgmName = Enum.GetName(typeof(CRIAudioList), index);
        if (bgmName != null)
        {
            _criAtomExPlaybackBGM = _criAtomSourceBgm.Play(bgmName);
        }
        else
        {
            Debug.LogError("指定されたCRIAudioListのenumが見つかりません。");
        }
    }

    public void CrisePlay(CRIAudioList index)
    {
        string bgmName = Enum.GetName(typeof(CRIAudioList), index);
        if (bgmName != null)
        {
            _criAtomSourceSe.Play(bgmName);
        }
        else
        {
            Debug.LogError("指定されたCRIAudioListのenumが見つかりません。");
        }
    }
}
