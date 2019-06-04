using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class csvUpdatePanel : MonoBehaviour
{


    public ChildReference list;
    private ChildReference obj;
   
    public List<Dictionary<string, object>> goldData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> Read = new List<Dictionary<string, object>>();


    public static int ChildCount;

    public ChildReference[] childReferences;

    
    private void Awake()
    {
        childReferences = GetComponentsInChildren<ChildReference>();
        UpdataPanel_Read();
        Update_Load();
        
    }

    public void OnEnable()
    {
        // ChildReference.UserDataNUpdateData();
        //for (int i = 0; i < ChildCount; i++)
        //{
        //    childReferences[i].ability2.text =
        //        UserDataMansger.Instance.updateData[i].Data.ToString();
        //}
    }
    private void Start()
    {
        //Update_Load2();
        //ChildReference.UserDataNUpdateData();
    }
    bool flag = false;
    private void Update()
    {
        if (flag) return;

        //if (childReferences != null)
        {
            Debug.Log("됫냐?");
            //if (File.Exists(UserDataMansger.Instance.getPath(UserDataMansger.Instance.userdataname)))
            //{
            //    for (int i = 0; i < UserDataMansger.Instance.updateData.Count; i++)
            //    {
            //        UserDataMansger.Instance.updateData[i].OBJ.ability2.text =
            //        UserDataMansger.Instance.updateData[i].Data.ToString();
            //    }
            //    ChildReference.UserDataNUpdateData();
            //}
            //else
            //{
            //    for (int i = 0; i < UserDataMansger.Instance.updateData.Count; i++)
            //    {
            //        UserDataMansger.Instance.updateData[i].Data = float.Parse(UserDataMansger.Instance.updateData[i].OBJ.ability2.text);
            //    }
            //    ChildReference.UserDataNUpdateData(0, 0, ChildReference.haveFile);
            //}

              ChildReference.UserDataNUpdateData(0, 0, ChildReference.haveFile);
            
            flag = true;
        }
    }

    public void UpdataPanel_Read()
    {
        Read = CSVReader.Read("UpgradeTextData"); //CsvRead.Instance.CsvReadUpgradeData;
        goldData = CSVReader.Read("UpdateDate");
        ChildCount = Read.Count;

      

        for (int i = 0; i < ChildCount; i++)
        {
            #region 레벨업데이타셋팅
            if (UserDataMansger.Instance.LvDataList.Count <= ChildCount)
            {
                LvUpData data = new LvUpData();
                UserDataMansger.Instance.LvDataList.Add(data);
            }
            #endregion

            childReferences[i].name = i.ToString();
            childReferences[i].ArrNumber = i;
           
            childReferences[i].Name.text = Read[i]["statName"].ToString();
            //float v = float.Parse(Read[i]["stat"].ToString()); /*+ LobyDataManager.Instance.EquipInfo.AddHp;*/
            childReferences[i].ability.text = Read[i]["stat"].ToString();
            

            //childReferences[i].Key = obj.ability.text;
            
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60.0f * Read.Count);


        //for (int i = 0; i < ChildCount; i++)
        //{
        //    childReferences[i].ability2.text =
        //        UserDataMansger.Instance.updateData[i].Data.ToString();
        //}





    }

    public UpdateData DATA(ChildReference obj,int LvCount)
    {
        UpdateData data = new UpdateData();
        data.Index = obj.ArrNumber;
        data.Name = obj.Name.text;
        data.Data = float.Parse(goldData[LvCount][obj.ArrNumber + "_value"].ToString());

        data.OBJ = obj;

        return data;
    }
    //유니티이벤트로 넣음 
    public void Update_Load()
    {
        for (int i = 0; i < ChildCount; i++)
        {
                int LvCount = UserDataMansger.Instance.LvDataList[i].LvCount;

                childReferences[i].GoldButtonText.text
                = goldData[LvCount][i + "_money"].ToString();

                childReferences[i].ability2.text
                = goldData[LvCount][i + "_value"].ToString();

                UserDataMansger.Instance.updateData.Add(DATA(childReferences[i], LvCount));
                //childReferences[i].Update_Load();
        }


        float addhp =0;
        if (LobyDataManager.Instance.EquipInfo!= null)
         addhp = LobyDataManager.Instance.EquipInfo.AddHp;
        

        //childReferences[0].ability2.text = UserDataMansger.Instance.userData.userAbillity.MaxHp.ToString();
        childReferences[0].ability2.text = (float.Parse(childReferences[0].ability2.text) + addhp).ToString();

            //ChildReference.UserDataNUpdateData();
            //ChildReference.UpdateDataNUserData();
    }


   





}