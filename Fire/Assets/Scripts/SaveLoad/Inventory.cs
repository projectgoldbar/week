using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<int> equipedItemIndexToItemList;

    public Player player;
    public List<Item_Equip> equipItem;

    public List<Item_Equip> itemList;
    public List<Image> uiImageList;
    public List<Image> equipedImageList;
    public GameObject Info;
    public Text hpText;
    public Text descriptionText;
    public Text Name;

    public int itemListIdx = 0;

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
        for (int i = 0; i < equipItem.Count; i++)
        {
            equipedImageList[i].sprite = equipItem[i].itemImage;
        }
        GameManager.instance.playerHp = everyHp;
    }

    //해제
    public void UnEquipment(int index)
    {
        equipItem[index] = null;
    }

    //아이템정보출력
    public void ShowInfo(int index)
    {
        itemListIdx = index;
        hpText.text = itemList[index].hp.ToString();
        descriptionText.text = itemList[index].description;
        Name.text = itemList[index].name;

        Info.SetActive(true);
    }

    //장착
    public void Equipment()
    {
        switch (itemList[itemListIdx].type)
        {
            case EquipType.Head:
                equipItem[0] = itemList[itemListIdx];
                equipedItemIndexToItemList[0] = itemListIdx;
                RefreshStatus();

                SaveInventory();
                break;

            case EquipType.Body:
                equipItem[1] = itemList[itemListIdx];
                equipedItemIndexToItemList[1] = itemListIdx;

                RefreshStatus();
                SaveInventory();
                break;

            case EquipType.Shose:
                equipItem[2] = itemList[itemListIdx];
                equipedItemIndexToItemList[2] = itemListIdx;

                RefreshStatus();
                SaveInventory();
                break;

            default:
                break;
        }
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

        for (int i = 0; i < a.equipIndexList.Count; i++)
        {
            equipedItemIndexToItemList.Add(a.equipIndexList[i]);
            //Equipment();
        }
        for (int i = 0; i < equipedItemIndexToItemList.Count; i++)
        {
            itemListIdx = equipedItemIndexToItemList[i];
            Equipment();
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