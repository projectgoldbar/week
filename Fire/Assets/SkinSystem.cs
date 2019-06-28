using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSystem : MonoBehaviour
{
    public int selectedSkinIdx = 0;

    public GameObject skinPivot;
    public Button skinEquipButton;
    public Image equipButtonImage;
    public Text equipText;

    public SpriteData spriteData;

    public SKinInfo[] skinInfos;

    private void Awake()
    {
        spriteData = GetComponent<SpriteData>();
        skinInfos = skinPivot.GetComponentsInChildren<SKinInfo>();
    }

    public void EquipSkin()
    {
        UserDataManager.Instance.userData.equipedSkinIdx = selectedSkinIdx;
        Refresh();
    }

    public void Refresh()
    {
        if (selectedSkinIdx == UserDataManager.Instance.userData.equipedSkinIdx)
        {
            equipButtonImage.sprite = spriteData.equipedSprite;
            skinEquipButton.interactable = false;
            equipText.text = "장착중";
        }
        else
        {
            equipButtonImage.sprite = spriteData.unequipSprite;
            skinEquipButton.interactable = true;
            equipText.text = "장착하기";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //var x = ThrowRandomSkin();
            //Debug.Log(x);
        }
    }

    public SKinInfo ThrowRandomSkin()
    {
        List<SKinInfo> notHaveList = new List<SKinInfo>();
        for (int i = 0; i < skinInfos.Length; i++)
        {
            if (skinInfos[i].isHave == false)
            {
                notHaveList.Add(skinInfos[i]);
            }
        }
        var x = Random.Range(0, notHaveList.Count);
        //notHaveList[x].isHave = true;
        return notHaveList[x];
    }
}