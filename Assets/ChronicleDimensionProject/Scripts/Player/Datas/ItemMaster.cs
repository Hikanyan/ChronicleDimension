using System;

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