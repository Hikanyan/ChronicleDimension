using UnityEngine;

/// <summary>Playerの情報 </summary>
public class PlayerInfo
{
    //Playerのlevel
    public int level = 1;
    //Playerの経験値
    public int exp = 0;
    //Playerの最大経験値
    public int maxExp = 100;
    //Playerの名前
    public string name = "Player";
    
    //Playerのクリア率
    public float clearPercent = 0.0f;
    //Playerのランク
    public string rank = "F";
    
}

public class PlayData
{
    public string rank;
    public float clearPercent;
    public int score;
    public int judgeScores;
    public int combo;
}