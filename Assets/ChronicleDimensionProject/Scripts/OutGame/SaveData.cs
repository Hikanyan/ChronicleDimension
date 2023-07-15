using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SaveDataクラスはセーブデータのクラスです。
/// </summary>
[System.Serializable]
public class SaveData
{
    // ゲームの進行状況を保存する変数やプロパティを定義する
    public Sprite BackgroundImage = null;
    public Sprite CharacterStandImage = null;
    public Sprite CharacterFaceImage = null;
    public Sprite TextBox = null;
    public string PlayerName = "Default";
    public AudioClip BGM = null;
    public AudioClip SE = null;
    public List<RhythmGameScoreDate> ScoreData;
}
/// <summary>
/// RhythmGameScoreDateクラスはリズムゲームのスコアデータのクラスです。
/// </summary>
[System.Serializable]
public class RhythmGameScoreDate : ScriptableObject
{
    public int Score = 0;
    public int MaxCombo = 0;
    public int PerfectCount = 0;
    public int GreatCount = 0;
    public int GoodCount = 0;
    public int BadCount = 0;
    public int MissCount = 0;
    public int MaxScore = 0;
    
}