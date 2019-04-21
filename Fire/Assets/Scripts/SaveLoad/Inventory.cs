using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player player;
    public List<Item_Equip> equipItem;
    public List<Item_Equip> itemList;
    public List<Image> uiImageList;

    //장착
    public void Equipment(int index)
    {
        switch (itemList[index].type)
        {
            case EquipType.Head:
                equipItem[0] = itemList[index];
                RefreshStatus();
                SaveInventory();
                break;

            case EquipType.Body:
                equipItem[1] = itemList[index];
                RefreshStatus();

                break;

            case EquipType.Shose:
                equipItem[2] = itemList[index];
                RefreshStatus();

                break;

            default:
                break;
        }
    }

    private void Start()
    {
        GameManager.instance.PutItemOnInventory(itemList);
        SaveInventory();
        RefreshStatus();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadInventory();
        }
    }

    //능력치갱신
    public void RefreshStatus()
    {
        int everyHp = 0;
        for (int i = 0; i < equipItem.Count; i++)
        {
            everyHp += equipItem[i].hp;
        }
        for (int i = 0; i < itemList.Count; i++)
        {
            uiImageList[i].sprite = itemList[i].itemImage;
        }
        GameManager.instance.playerHp = everyHp;
    }

    //해제
    public void UnEquipment(int index)
    {
        equipItem[index] = null;
    }

    private void SaveInventory()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadInventory()
    {
        InventoryData a = SaveSystem.LoadPlayer();
        for (int i = 0; i < a.hp.Count; i++)
        {
            itemList.Add(new Item_Equip(a.type[i], a.name[i], a.description[i], a.hp[i]));
        }

        //for (int i = 0; i < a.itemlist.Count; i++)
        //{
        //    itemList.Add(a.itemlist[i]);
        //}
        //for (int i = 0; i < a.equipList.Count; i++)
        //{
        //    equipItem.Add(a.equipList[i]);
        //}
        RefreshStatus();
    }
}