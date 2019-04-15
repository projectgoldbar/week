using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserItemData : MonoBehaviour
{
    //public Queue<Item.BoxingItem> itembox;

    private void Awake()
    {
        //itembox = new Queue<Item.BoxingItem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            출력();
        }
    }

    public void 출력()
    {
    }
}