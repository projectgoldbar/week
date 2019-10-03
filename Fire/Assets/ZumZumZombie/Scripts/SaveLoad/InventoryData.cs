using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<int> hp;
    public List<string> name;
    public List<EquipType> type;
    public List<string> description;
    public List<int> equipIndexList;

    public InventoryData(Inventory inventory)
    {
        hp = new List<int>();
        name = new List<string>();
        type = new List<EquipType>();
        description = new List<string>();
        equipIndexList = new List<int>();

        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            hp.Add(inventory.itemList[i].hp);
            name.Add(inventory.itemList[i].name);
            type.Add(inventory.itemList[i].type);
            description.Add(inventory.itemList[i].description);
        }

        //if (inventory.equipedItemIndexToItemList != null)
        //{
        //    for (int i = 0; i < inventory.equipedItemIndexToItemList.Count; i++)
        //    {
        //        equipIndexList.Add(inventory.equipedItemIndexToItemList.Dequeue());
        //    }
        //}
    }
}