using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class EquipRefData
{
    public string Name;
    public string ability;
    public string ability2;
    public string Text;
}
public class ChildReference1 : MonoBehaviour
{
    public int ArrNumber;

    public Text Name = null;
    public Text ability = null;
    public Text ability2 = null;
    public Button GoldButton = null;


    public Text GoldButtonText = null;
      
    public Image Buttonimage = null;
         

    private panelOnoff panel;
    public CsvEquipPanel csvEquip;

    public float AddHp;
    public bool b_Panel;
    public bool b_Collection;

    public List<EquipDataSet> DataListSet = new List<EquipDataSet>();

    public EquipRefData equipRef = new EquipRefData();


    public static int EquipIndex;
    private void Awake()
    {
        panel = FindObjectOfType<panelOnoff>();
        GoldButton.onClick.AddListener(DataEquip);
        
    }

    private void OnEnable()
    {
        ArrNumber = int.Parse(transform.name);
        EquipIndex = - 1;
        defaultButtonColor();
        EquipLoadButtonColorText();
        //EquipButtonText();
    }

    

    public void DataEquip()
    {
        ResetData(ArrNumber);

        var DataListValue = LobyDataManager.Instance.reference1[ArrNumber];

        UserDataMansger.Instance.userData.EquipInfo = DataListValue;

        for (int i = 0; i < DataListValue.DataListSet.Count; i++)
        {
            var data = DataListValue.DataListSet[i].Data;
            var key = DataListValue.DataListSet[i].Key;

            UserDataMansger.Instance.userData.skillLVList[key] = data;
        }

        ChildReference.UpdateDataNUserData(DataListValue.AddHp);
        //ChildReference.UserDataNUpdateData(DataListValue.AddHp);
        DataListValue.b_Panel = true;


        UserDataMansger.Instance.userData.skillEquip[ArrNumber] =
            LobyDataManager.Instance.reference1[ArrNumber].b_Panel;



        EquipButtonText(ArrNumber);


        UserDataMansger.Instance.UserDataBinarySave(UserDataMansger.Instance.userdataname);

        //csvEquip.ChangeModel.sharedMesh = csvEquip.meshRenderer[ArrNumber%csvEquip.meshRenderer.Length].sharedMesh;
        //UserDataMansger.userData.skillLVList[돌연변이 index] = 돌연변이레벨;
        //스킨장착 후 데이터 변동을 해야하나?....
    }


   



    private void ResetData(int arrnum = -1)
    {

        for (int i = 0; i < LobyDataManager.Instance.reference1.Length; i++)
        {
            if (!LobyDataManager.Instance.reference1[i].b_Panel) { continue; }

            LobyDataManager.Instance.reference1[i].b_Panel = false;
            UserDataMansger.Instance.userData.skillEquip[i]
                        = LobyDataManager.Instance.reference1[i].b_Panel;
        }


        for (int i = 0; i < UserDataMansger.Instance.userData.skillLVList.Length; i++)
        {
            if (UserDataMansger.Instance.userData.skillLVList[i] == 0) { continue; }

                UserDataMansger.Instance.userData.skillLVList[i] = 0;
            
        }
    }



    private void defaultButtonColor()
    {
        for (int i = 0; i < UserDataMansger.Instance.userData.skillEquip.Length; i++)
        {
            if (UserDataMansger.Instance.userData.skillCollection[i]) //수집
            {
                LobyDataManager.Instance.reference1[i].Buttonimage.color = Color.green;
            }
        }
    }


    private void EquipButtonText(int arrnum = -1)
    {

        LobyDataManager.Instance.reference1[arrnum].GoldButtonText.text = "장착중";
        LobyDataManager.Instance.reference1[arrnum].Buttonimage.color = Color.red;
        LobyDataManager.Instance.reference1[arrnum].Buttonimage.raycastTarget = false;


        if (EquipIndex != -1)
        {
            LobyDataManager.Instance.reference1[EquipIndex].GoldButtonText.text = "장착";
            LobyDataManager.Instance.reference1[arrnum].Buttonimage.raycastTarget = true;
            LobyDataManager.Instance.reference1[EquipIndex].Buttonimage.color = Color.green;
        }

        EquipIndex = arrnum;
    }


    private void EquipLoadButtonColorText()
    {
        for (int i = 0; i < UserDataMansger.Instance.userData.skillEquip.Length; i++)
        {
            if (UserDataMansger.Instance.userData.skillCollection[i]) //수집
            {
                if (UserDataMansger.Instance.userData.skillEquip[i]) //장착
                {
                    EquipIndex = i;
                    LobyDataManager.Instance.reference1[i].GoldButtonText.text = "장착중";
                    LobyDataManager.Instance.reference1[i].Buttonimage.color = Color.red;
                }
            }
            else
            {
                LobyDataManager.Instance.reference1[i].GoldButtonText.text = "";
            }
        }
    }


}