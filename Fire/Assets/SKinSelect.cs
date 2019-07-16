using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SKinSelect : MonoBehaviour
{
    public SkinSelectUI[] skins;

    public Button[] skinsImage;

    private void Awake()
    {

    }



    public void CanvasGroupNImageColor(int n)
    {
        var a = skinsImage[n].colors;
        a.colorMultiplier = 5.0f;
        skinsImage[n].colors = a;

        for (int i = 0; i < skins.Length; i++)
        {
            if (n == i) continue;

            if (UserDataManager.Instance.userData.gainSkin[i] == true)
            {
                var b = skinsImage[i].colors;
                b.colorMultiplier = 1;
                skinsImage[i].colors = b;
            }


        }
    }



}
