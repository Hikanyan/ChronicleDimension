using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ChronicleDimension.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;



// -------------------------------------------
// SAVE DATA
// -------------------------------------------

[Serializable]
public class SaveData : MasterDataBase
{
    public PlayerInfo playerInfo;
    public PlayerSettings playerSettings;
    public RhythmGameMaster rhythmGameMaster;
    public ActionGameMaster actionGameMaster;
    public NovelGameMaster novelGameMaster;
    public Sprite backgroundImage;
    public Sprite characterStandImage;
    public string playerName = "Default";
    public string bgm;
    public string se;
}


