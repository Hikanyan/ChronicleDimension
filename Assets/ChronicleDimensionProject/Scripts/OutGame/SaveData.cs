using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class MasterVersion
{
    public string masterName;
    public int version;
}
[Serializable]
public class MasterDataBase
{
    public int Version;
}
[Serializable]
public class TextData : MasterDataBase
{
    public string Text;
}

/// <summary>
/// SaveDataクラスはセーブデータのクラスです。
/// </summary>
[System.Serializable]
public class SaveData: MasterDataBase
{
    // ゲームの進行状況を保存する変数やプロパティを定義する
    public List<RhythmGameDate> RhythmGameDate;
    public Sprite BackgroundImage = null;
    public Sprite CharacterStandImage = null;
    public Sprite CharacterFaceImage = null;
    public Sprite TextBox = null;
    public string PlayerName = "Default";
    public string BGM = null;
    public string SE = null;
}
/// <summary>
/// RhythmGameScoreDateクラスはリズムゲームのスコアデータのクラスです。
/// </summary>
[CreateAssetMenu(fileName = "RhythmGameDate", menuName = "ScriptableObjects/CreateRhythmGameDateAsset")]
public class RhythmGameDate : ScriptableObject
{
    public AssetReferenceT<TextAsset> _musicJsonReference;
    public int MusicNumber = default;//CRI
    public float DelayTime = 0.0f;
    
    public int MaxScore = 0;
    public int MaxCombo = 0;
    
    public int PerfectCount = 0;
    public int GreatCount = 0;
    public int GoodCount = 0;
    public int BadCount = 0;
    public int MissCount = 0;
    
}
[Serializable]
public class EventMaster : MasterDataBase
{
    public EventData[] Data;
}

[Serializable]
public class EventData
{
    public int Id;
    public string Name;
    public string Resource;
    public string StartAt;
    public string GameEndAt;
    public string EndAt;
}
public class GameEvent
{
    public int Id;
    public string Name;
    public string Resource;
    public DateTime StartAt;
    public DateTime GameEndAt;
    public DateTime EndAt;
}

[Serializable]
public class QuestMaster : MasterDataBase
{
    public QuestData[] Data;
}

[Serializable]
public class QuestData
{
    public int Id;
    public string Name;
    public string Resource;
    public DateTime StartAt;
    public DateTime GameEndAt;
    public DateTime EndAt;
}

[Serializable]
public class ItemMaster : MasterDataBase
{
    public ItemData[] Data;
}

[Serializable]
public class ItemData
{
    public int Id;
    public string Name;
    public string Resource;
    public string Description;
    public int Price;
    public int MaxCount;
    public int MaxLevel;
    public int Rarity;
    public int Type;
    public int[] Effect;
}