using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionUIController : MonoBehaviour
{
    public Image triangleOutline_Image;
    public SpriteRenderer doorOutline_SpriteRenderer;

    public void OffTriangleOutlineforDownPanel()
    {
        triangleOutline_Image.GetComponent<Image>().enabled = false;
    }

    public void OnTriangleOutlineforDownPanel()
    {
        triangleOutline_Image.GetComponent<Image>().enabled = true;
    }
}