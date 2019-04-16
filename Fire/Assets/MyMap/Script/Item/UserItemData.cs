using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserItemData : MonoBehaviour
{
    public static UserItemData instance = null;

    public List<Item> inventory;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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