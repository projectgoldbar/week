using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelectUI : MonoBehaviour
{
    // Start is called before the first frame update
    public SKinSelect skinselect;

    public int index;


    private void Start()
    {
        skinselect.CanvasGroupNImageColor(UserDataManager.Instance.userData.equipedSkinIdx);
    }


    public void Click()
    {
        //Debug.Log(index);
        //skinselect.CanvasGroupNImageColor(UserDataManager.Instance.userData.equipedSkinIdx);
        skinselect.CanvasGroupNImageColor(index);

    }
}
