using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilKM
{
    public static void SetGlobalManagerByAwake()

    {
        GameObject noRemoveManager = GameObject.Find("NoRemoveManager");

        if (null == noRemoveManager.GetComponent<GameInfo>())
        {
            noRemoveManager.AddComponent<GameInfo>();
        }

        if (null == noRemoveManager.GetComponent<CSVManager>())
        {
            noRemoveManager.AddComponent<CSVManager>();
        }
    }
}