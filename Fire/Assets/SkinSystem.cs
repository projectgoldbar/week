﻿using System.Collections;
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
    public SkinnedMeshRenderer MeshData;

    //public SkinnedMeshRenderer[] playerSkins;
    public GameObject playerSkinPivot;

    private void Awake()
    {
        spriteData = GetComponent<SpriteData>();
        skinInfos = skinPivot.GetComponentsInChildren<SKinInfo>();
        ChangeMesh();
    }

    public void EquipSkin()
    {
        UserDataManager.Instance.userData.equipedSkinIdx = selectedSkinIdx;
        ChangeMesh();
        Refresh();
    }

    public void ChangeMesh()
    {
        MeshData.sharedMesh =
            UserDataManager.Instance.EquipSkinReference[UserDataManager.Instance.userData.equipedSkinIdx].sharedMesh;

        //for (int i = 0; i < playerSkins.Length; i++)
        //{
        //    if (playerSkins[i].gameObject.activeSelf)
        //    {
        //        playerSkins[i].gameObject.SetActive(false);
        //    }
        //}
        //playerSkins[selectedSkinIdx].gameObject.SetActive(true);
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

        if (notHaveList.Count == 0)
        {
            return null;
        }

        var x = Random.Range(0, notHaveList.Count);
        //notHaveList[x].isHave = true;
        return notHaveList[x];
    }
}