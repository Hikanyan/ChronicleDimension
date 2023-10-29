using System;
using UnityEngine;

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