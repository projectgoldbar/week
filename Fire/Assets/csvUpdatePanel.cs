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

    }

    public void OnEnable()
    {
        UpdataPanel_Read();

        if (File.Exists(UserDataMansger.Instance.getPath(UserDataMansger.Instance.userdataname)))
        {

        }
        else
        {
            Update_Load();
        }

       
    }




    public void UpdataPanel_Read()
    {
        Read = CSVReader.Read("UpgradeTextData"); //CsvRead.Instance.CsvReadUpgradeData;
        goldData = CSVReader.Read("UpdateDate");
        ChildCount = Read.Count;

        Debug.Log(ChildCount);
        for (int i = 0; i < ChildCount; i++)
        {
            #region 레벨업데이타셋팅
            LvUpData data = new LvUpData();
            UserDataMansger.Instance.LvDataList.Add(data);
            #endregion
            //obj = GameObject.Instantiate<ChildReference>(list, transform);

            childReferences[i].name = i.ToString();
            childReferences[i].ArrNumber = i;
           
            childReferences[i].Name.text = Read[i]["statName"].ToString();
            childReferences[i].ability.text = Read[i]["stat"].ToString();
            

            //childReferences[i].Key = obj.ability.text;
            
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60.0f * Read.Count);

        
    }

    public UpdateData DATA(ChildReference obj)
    {
        UpdateData data = new UpdateData();
        data.Index = obj.ArrNumber;
        data.Name = obj.Name.text;
        data.Data = float.Parse(obj.ability2.text);

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
                = goldData[LvCount][i + "_value"].ToString()+"";

            UserDataMansger.Instance.updateData.Add(DATA(childReferences[i]));

        }
        ChildReference.UpdateDataNUserData();
        //ChildReference.UserDataNUpdateData();
    }
}