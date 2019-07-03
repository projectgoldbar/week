using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionUIController : MonoBehaviour
{
    public Image triangleOutline_Image;
    public DoorOutLine doorOutLine;

    public void OffTriangleOutlineforDownPanel()
    {
        triangleOutline_Image.GetComponent<Image>().enabled = false;
    }

    public void OnTriangleOutlineforUpPanel()
    {
        triangleOutline_Image.GetComponent<Image>().enabled = true;
    }

    public void OnClickedforLeaveLobby()
    {
        doorOutLine.ReactforLeaveLobby();
    }
}