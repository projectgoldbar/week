using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiSelectColor : MonoBehaviour
{
    public uiSelect canvasGroup;

    

    public int n;

    public bool flag = false;

    private void Awake()
    {
        n = transform.GetSiblingIndex();
    }


    public void Click()
    {
        flag = !flag;
        canvasGroup.CanvasGroupAlphaOn(n, flag);
    }

}
