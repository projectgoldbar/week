using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiSelectColor : MonoBehaviour
{
    public uiSelect canvasGroup;

    //private bool clicked;
    //public bool Clicked
    //{
    //    get { return clicked; }
    //    set
    //    {
    //        clicked = value;
    //        if (clicked) canvasGroup.alpha = 1.0f;
    //        else canvasGroup.alpha = 0.2f;
    //    }
    //}

    // Start is called before the first frame update

    public int n;

    private void Awake()
    {
        n = transform.GetSiblingIndex();
    }


    public void Click()
    {
        canvasGroup.CanvasGroupAlphaOn(n);
    }

}
