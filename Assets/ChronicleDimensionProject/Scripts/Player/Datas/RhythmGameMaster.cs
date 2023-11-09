using System;
using ChronicleDimension.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class RhythmGameMaster : MasterDataBase
{
    public RhythmGameData[] rhythmGameDatas;
}

[CreateAssetMenu(fileName = "RhythmGameData", menuName = "ScriptableObjects/CreateRhythmGameDataAsset")]
public class RhythmGameData : ScriptableObject
{
    [SerializeField] private MusicData musicData;

    public MusicData MusicData => musicData;

    public int maxScore;
    public int maxCombo;
    public int perfectCount;
    public int greatCount;
    public int goodCount;
    public int badCount;
    public int missCount;
}

[CreateAssetMenu(fileName = "RhythmGameMusicData", menuName = "ScriptableObjects/CreateRhythmGameDataAsset")]
public class MusicData : ScriptableObject
{
    public AssetReferenceT<TextAsset> musicJsonReference;

    public CriAudioManager.CueSheet cueSheet;
    public string musicName;
    public float delayTime;
    public bool autoMode;
}

public struct ResultDatas
{
    public JudgeScores judgeScores;
    public int maxCombo;
    public int score;
    public float clearPercent;
    public string musicName;
    public string rank;
}