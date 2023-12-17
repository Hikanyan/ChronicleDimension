using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace ChronicleDimensionProject.Player
{
    [Serializable]
    public class MasterVersion
    {
        public string masterName;
        public int version;
    }

    [Serializable]
    public class MasterDataBase
    {
        [FormerlySerializedAs("Version")] public int version;
    }


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
        public bool autoMode;
    }
    public class RhythmGameResultDatas
    {
        public string rank;
        public float clearPercent;
        public int score;
        public JudgeScores judgeScores;
        public int maxCombo;
    }
    public class RhythmGameResultData
    {
        public string musicName;
        public int score;
        public int maxScore;
        public int maxCombo;
        public int perfectCount;
        public int greatCount;
        public int goodCount;
        public int badCount;
        public int missCount;
        public bool autoMode;
    }

    [Serializable]
    public class ActionGameMaster : MasterDataBase
    {
        public ActionGameData[] actionGameDatas;
    }

    [Serializable]
    public class ActionGameData : ScriptableObject
    {
    }

    [Serializable]
    public class NovelGameMaster : MasterDataBase
    {
        public NovelGameData[] novelGameDatas;
    }

    [Serializable]
    public class NovelGameData : ScriptableObject
    {
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
    public class ItemMaster : MasterDataBase
    {
        public ItemData[] itemDatas;
    }

    [Serializable]
    public class ItemData
    {
        public int id;
        public string name;
        public int maxCount;
        public int maxLevel;
        public int rarity;
        public int type;
        public int[] effect;
    }
}