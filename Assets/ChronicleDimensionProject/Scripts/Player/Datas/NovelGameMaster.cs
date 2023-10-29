using System;
using UnityEngine;

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