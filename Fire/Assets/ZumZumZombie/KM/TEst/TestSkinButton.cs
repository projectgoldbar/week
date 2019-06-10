using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkinButton : MonoBehaviour
{
    public Text skinIDTxt;
    public Text skinIndexTxt;
    private int skinID;
    private SkinsKeyCSV skinsCSV;

    private void Start()
    {
    }

    public void GetSkinInfo()
    {
        //skinID = System.Convert.ToInt32(skinIDTxt);
        skinsCSV = CSVManager.Instance.GetSkinsKeyCSV(skinID);
        skinIndexTxt.text = string.Format("skinIndex : {0}", skinsCSV.skinIndex);
    }
}