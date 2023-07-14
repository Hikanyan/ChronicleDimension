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
}