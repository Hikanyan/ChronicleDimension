using System;

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