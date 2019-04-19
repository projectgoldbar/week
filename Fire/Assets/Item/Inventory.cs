using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player player;
    public Item[] equipMentList;

    public GameObject[] inventory;

    private void Update()
    {
        //inventory[i].GetComponent<Item>()
    }

    //장착
    public void Equipment(Item item)
    {
    }

    //능력치갱신

    //해제
    public void UnEquipment(Item item)
    {
    }
}