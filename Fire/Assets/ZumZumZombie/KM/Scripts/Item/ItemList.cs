using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemList : Singleton<ItemList>
{
    public List<EquipItemInfo> equipItemList;
}

[System.Serializable]
public class EquipItemInfo
{
    public string name;
    public string description;
    public EquipType type;
    public int minStat = 0;
    public int maxStat = 3;
}