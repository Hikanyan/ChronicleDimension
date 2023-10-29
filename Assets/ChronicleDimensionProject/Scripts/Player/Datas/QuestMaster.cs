using System;

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