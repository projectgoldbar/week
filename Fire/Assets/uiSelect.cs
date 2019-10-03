using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSelect : MonoBehaviour
{
    public CanvasGroup[] groups;


    public void CanvasGroupAlphaOn(int n , bool flag)
    {
       
        if(flag)
        groups[n].alpha = 0.2f;
        else
        groups[n].alpha = 1;


        for (int i = 0; i < groups.Length; i++)
        {
            if (n != i)
            {
                groups[i].alpha = 1;
                groups[i].GetComponent<UiSelectColor>().flag = false;
            }
        }

    }


}
