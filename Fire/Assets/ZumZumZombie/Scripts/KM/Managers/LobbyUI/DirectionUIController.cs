using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionUIController : MonoBehaviour
{
    public Image triangleOutline_Image;
    public Image triangle_Image;
    public DoorOutLine doorOutLine;

    public void OffTriangleandOutlineforDownPanel()
    {
        triangleOutline_Image.enabled = false;
        triangle_Image.enabled = false;
    }

    public void OnTriangleOutlineforUpPanel()
    {
        triangleOutline_Image.enabled = true;
        triangle_Image.enabled = true;
    }

    public void OnClickedforLeaveLobby()
    {
        doorOutLine.ReactforLeaveLobby();
    }
}