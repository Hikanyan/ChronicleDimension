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
public class SaveData : MasterDataBase
{
    public RhythmGameMaster rhythmGameMaster;
    public ActionGameMaster actionGameMaster;
    public NovelGameMaster novelGameMaster;
    public Sprite backgroundImage;
    public Sprite characterStandImage;
    public string playerName = "Default";
    public string bgm;
    public string se;
}

[Serializable]
public class RhythmGameMaster : MasterDataBase
{
    public RhythmGameData[] rhythmGameDatas;
}

[CreateAssetMenu(fileName = "RhythmGameData", menuName = "ScriptableObjects/CreateRhythmGameDataAsset")]
public class RhythmGameData : ScriptableObject
{
    public AssetReferenceT<TextAsset> musicJsonReference;
    public string musicName;
    public float delayTime;
    public int maxScore;
    public int maxCombo;
    public int perfectCount;
    public int greatCount;
    public int goodCount;
    public int badCount;
    public int missCount;
}

[Serializable]
public class ActionGameMaster : MasterDataBase
{
    public ActionGameData[] actionGameDatas;
}

[Serializable]
public class ActionGameData : ScriptableObject
{
    // Add ActionGameData fields here (e.g., score, level, etc.)
}

[Serializable]
public class NovelGameMaster : MasterDataBase
{
    public NovelGameData[] novelGameDatas;
}

[Serializable]
public class NovelGameData : ScriptableObject
{
    // Add NovelGameData fields here (e.g., currentChapter, choicesMade, etc.)
}

[Serializable]
public class GameEventMaster : MasterDataBase
{
    public GameEventData[] gameEventDatas;
}

[Serializable]
public class GameEventData
{
    public int id;
    public string name;
    public string resource;
    public DateTime startAt;
    public DateTime gameEndAt;
    public DateTime endAt;
}

[Serializable]
public class QuestMaster : MasterDataBase
{
    public QuestData[] questDatas;
}

[Serializable]
public class QuestData
{
    public int id;
    public string name;
    public string resource;
    public DateTime startAt;
    public DateTime gameEndAt;
    public DateTime endAt;
}

[Serializable]
public class ItemMaster : MasterDataBase
{
    public ItemData[] itemDatas;
}

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public string resource;
    public string description;
    public int price;
    public int maxCount;
    public int maxLevel;
    public int rarity;
    public int type;
    public int[] effect;
}
