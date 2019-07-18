using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{
    public ShopButton[] sbs;
    public Button[] shopbuttons;
    public Text[] texts;

    public void AllBlock()
    {
        for (int i = 0; i < 2; i++)
        {
            sbs[i].Block();
        }
    }
}
