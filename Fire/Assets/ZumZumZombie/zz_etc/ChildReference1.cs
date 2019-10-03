using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

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
    private void Start()
    {
      
    }


    public void DataEquip()
    {
        UserDataMansger.Instance.userData.userAbillity.EquipIndex = ArrNumber;

        if (LobyDataManager.Instance.reference1[ArrNumber].b_Panel) return;

        ResetData(ArrNumber);

        var DataListValue = LobyDataManager.Instance.reference1[ArrNumber];

        LobyDataManager.Instance.EquipInfo = DataListValue;



        for (int i = 0; i < DataListValue.DataListSet.Count; i++)
        {
            var data = DataListValue.DataListSet[i].Data;
            var key = DataListValue.DataListSet[i].Key;

            UserDataMansger.Instance.userData.skillLVList[key] = data;
        }

        DataListValue.b_Panel = true;


        UserDataMansger.Instance.userData.skillEquip[ArrNumber] =
            LobyDataManager.Instance.reference1[ArrNumber].b_Panel;

        EquipButtonText(ArrNumber);


        //ChildReference.UserDataNUpdateData(LobyDataManager.Instance.EquipInfo.AddHp,0,ChildReference.haveFile);
        ChildReference.UserDataNUpdateData(LobyDataManager.Instance.EquipInfo.AddHp,0, ChildReference.haveFile);
        ChildReference.UpdateDataNUserData();


        //DataListValue.ability2.text 
        //    = UserDataMansger.Instance.userData.userAbillity.MaxHp.ToString();

        //csvEquip.ChangeModel.sharedMesh = csvEquip.meshRenderer[ArrNumber%csvEquip.meshRenderer.Length].sharedMesh;
        //UserDataMansger.userData.skillLVList[돌연변이 index] = 돌연변이레벨;
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
       

        if (EquipIndex != -1)
        {
            LobyDataManager.Instance.reference1[EquipIndex].GoldButtonText.text = "장착";
            LobyDataManager.Instance.reference1[EquipIndex].Buttonimage.color = Color.green;

            UserDataMansger.Instance.userData.userAbillity.MaxHp -= LobyDataManager.Instance.reference1[EquipIndex].AddHp;
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