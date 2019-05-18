using System.Collections.Generic;
using UnityEngine;


public class CSVdata : MonoBehaviour
{
    public ChildReference list;
    public string CSVFileName;
    public string CSVFileGoldData;

    private ChildReference obj;
   
    public List<Dictionary<string, object>> goldData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> Read = new List<Dictionary<string, object>>();


    public static int ChildCount;

    public void UpdataPanel_Read()
    {
        Read = CSVReader.Read(CSVFileName);
        goldData = CSVReader.Read(CSVFileGoldData);
        ChildCount = Read.Count;
        for (int i = 0; i < Read.Count; i++)
        {
            #region 레벨업데이타셋팅
            LvUpData data = new LvUpData();
            UserDataMansger.Instance.LvDataList.Add(data);
            #endregion

            obj = GameObject.Instantiate<ChildReference>(list, transform);
            obj.name = i.ToString();
            obj.Name.text = Read[i]["이름Text"].ToString();
            obj.ability.text = Read[i]["적용능력Text"].ToString() + "";
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60.0f * Read.Count);
    }

    int cnt;
    public void UpdateDateRead()
    {
        var ArrNumber = int.Parse(transform.name);

        if (goldData.Count - 1 == cnt)
        {
            obj.GoldButtonText.text = "Max";
            obj.ability2.text = "+" + goldData[cnt][ArrNumber + "번능력치"].ToString();
            return;
        }

        if (goldData.Count > cnt)
        {
            obj.GoldButtonText.text = goldData[cnt][ArrNumber + "번금액"].ToString();
            obj.ability2.text = "+" + goldData[cnt][ArrNumber + "번능력치"].ToString();
            cnt++;
        }
        obj.LvCount = cnt;
    }


    public void UpdateDateRead2() { }

}
    