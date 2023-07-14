using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveData : ScriptableObject
{
    // ここにゲームの進行状況を保存する変数やプロパティを定義する
    [SerializeField] Image BackgroundImage;
    [SerializeField] Image CharacterStandImage;
    [SerializeField] Image CharacterFaceImage;
    [SerializeField]  Image TextBox;
    
    [SerializeField] string PlayerName;
    [SerializeField] AudioClip BGM;
    [SerializeField] AudioClip SE;

    public SaveData()
    {
        
    }
}