using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyTimeCal : MonoBehaviour
{
    public int nokoriTime;
    public int sleepTime;
    public int eatTime;
    public int nokoriday;

    public int happyTime;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            happyTime = nokoriTime - ((sleepTime * nokoriday) + (eatTime * nokoriday));
        }
    }
}