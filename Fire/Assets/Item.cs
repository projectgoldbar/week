using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float index = 0;
    public UserItemData userItemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //ItemTypeChk();
            //if (index == 0)
            //{
            //    var supplies = new Supplies();
            //    Debug.Log("이 아이템은 소모품");
            //    ItemAdd(supplies);
            //}
            //else
            //{
            //    var equipitem = new EquipItem();
            //    Debug.Log("이 아이템은 장비");
            //    ItemAdd(equipitem);
            //}
            //Destroy(gameObject);
        }
    }

    private void ItemTypeChk()
    {
        index = Random.Range(0, 2);
    }

    //private void ItemAdd(BoxingItem a)
    //{
    //    userItemData.itembox.Enqueue(a);
    //}

    //public class BoxingItem
    //{
    //    public BoxingItem UnBoxing()
    //    {
    //        var a = Random.Range(1, 101);
    //        if (a < 50)
    //        {
    //            return new EquipItem();
    //        }
    //        else
    //        {
    //            return new Supplies();
    //        }
    //    }
    //}

    //public class Supplies : BoxingItem
    //{
    //    private int gold = 0;

    //    public Supplies()

    //    {
    //        gold = Random.Range(1, 20);
    //    }
    //}

    //public class EquipItem : BoxingItem
    //{
    //    public int hp = 0;

    //    public EquipItem()
    //    {
    //        RandomParametor();
    //    }

    //    public void RandomParametor()
    //    {
    //        hp = Random.Range(1, 5);
    //    }
    //}
}