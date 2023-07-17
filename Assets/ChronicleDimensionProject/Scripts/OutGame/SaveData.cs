using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
/// <summary>
/// SaveDataクラスはセーブデータのクラスです。
/// </summary>
[System.Serializable]
public class SaveData
{
    // ゲームの進行状況を保存する変数やプロパティを定義する
    public List<RhythmGameDate> RhythmGameDate;
    public Sprite BackgroundImage = null;
    public Sprite CharacterStandImage = null;
    public Sprite CharacterFaceImage = null;
    public Sprite TextBox = null;
    public string PlayerName = "Default";
    public AudioClip BGM = null;
    public AudioClip SE = null;
}
/// <summary>
/// RhythmGameScoreDateクラスはリズムゲームのスコアデータのクラスです。
/// </summary>
[CreateAssetMenu(fileName = "RhythmGameDate", menuName = "ScriptableObjects/CreateRhythmGameDateAsset")]
public class RhythmGameDate : ScriptableObject
{
    public AssetReferenceT<TextAsset> _musicJsonReference;
    public int _musicNumber = default;//CRI
    public float _delayTime = 0.0f;
    
    public int MaxScore = 0;
    public int MaxCombo = 0;
    
    public int PerfectCount = 0;
    public int GreatCount = 0;
    public int GoodCount = 0;
    public int BadCount = 0;
    public int MissCount = 0;
    
}