using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public GameObject shopPanel;

    public void ShopOpen()
    {
        shopPanel.
            SetActive(true);
    }

    public void ShopClose()
    {
        shopPanel.SetActive(false);
    }
}