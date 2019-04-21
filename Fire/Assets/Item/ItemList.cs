using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}