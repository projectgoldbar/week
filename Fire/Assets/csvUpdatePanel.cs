using System.Collections.Generic;
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
    }



    public void UpdataPanel_Read()
    {
        var Read = CSVReader.Read("UpgradeTextData"); //CsvRead.Instance.CsvReadUpgradeData;
        var goldData = CSVReader.Read("UpdateDate");
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
            childReferences[i].Name.text = Read[i]["statName"].ToString();
            childReferences[i].ability.text = Read[i]["stat"].ToString();

            //childReferences[i].Key = obj.ability.text;

        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60.0f * Read.Count);
    }
}