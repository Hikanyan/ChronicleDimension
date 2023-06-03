using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
using UniRx;
using Cysharp.Threading.Tasks;
using Hikanyan.Core;

public class CRIAudioManager : AbstractSingleton<CRIAudioManager>
{
    string _streamingAssetsPathACF = "ChronicleDimension";
    string _cueSheetBGM = "CueSheet_BGM";
    string _cueSheetSE = "CueSheet_SE";
    
    CriAtomSource _criAtomSourceBgm;
    CriAtomSource _criAtomSourceSe;
    
    private CriAtomExPlayback _criAtomExPlaybackBGM;
    CriAtomEx.CueInfo _cueInfo;

    private void OnAwake()
    {
        //acf設定
        string path = Common.streamingAssetsPath + $"/{_streamingAssetsPathACF}.acf";

        CriAtomEx.RegisterAcf(null, path);

        // CriAtom作成
        new GameObject().AddComponent<CriAtom>();

        // BGM acb追加
        CriAtom.AddCueSheet(_cueSheetBGM, $"{_cueSheetBGM}.acb", null, null);
        // SE acb追加
        CriAtom.AddCueSheet(_cueSheetSE, $"{_cueSheetSE}.acb", null, null);


        //BGM用のCriAtomSourceを作成
        _criAtomSourceBgm = gameObject.AddComponent<CriAtomSource>();
        _criAtomSourceBgm.cueSheet = _cueSheetBGM;
        //SE用のCriAtomSourceを作成
        _criAtomSourceSe = gameObject.AddComponent<CriAtomSource>();
        _criAtomSourceSe.cueSheet = _cueSheetSE;
    }

    
    async UniTask CRIBGMPlay(int index, float delayTime)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delayTime));
        CRIBGMPlay(index);
    }
    
    void CRIBGMPlay(int index)
    {
        _criAtomExPlaybackBGM = _criAtomSourceBgm.Play(index);
    }

    void CRISEPlay(int index)
    {
        _criAtomSourceSe.Play(index);
    }
    
}
