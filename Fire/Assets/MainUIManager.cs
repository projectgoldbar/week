using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public GameObject InvenPanel;

    public void InvenOpen()
    {
        InvenPanel.
            SetActive(true);
    }

    public void InvenClose(GameObject gObject)
    {
        gObject.SetActive(false);
    }
}