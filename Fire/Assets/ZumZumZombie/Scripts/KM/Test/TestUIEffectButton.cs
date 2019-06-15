using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIEffectButton : MonoBehaviour
{
    public void OnTestButton()
    {
        //string stageText = Random.Range(0, 9).ToString();
        string stageText = "------\n ---------l";
        UITweenEffectManager.Instace.stageOpenPanel.OpenPanel(stageText);
    }

    public void OnSkinBoxOpen()
    {
        UITweenEffectManager.Instace.skinBoxOpenPanel.Open();
    }
}