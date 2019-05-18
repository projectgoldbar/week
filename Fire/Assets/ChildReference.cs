using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChildReference : MonoBehaviour
{

    public int LvCount;
    public int ArrNumber;

    public Text Name = null;
    public Text ability = null;
    public Text ability2 = null;
    public Button GoldButton = null;
    public Text GoldButtonText = null;

    public UnityEvent ReadData;
    private panelOnoff panel;
    private csvUpdatePanel csvUpdate;
    public List<Dictionary<string, object>> goldRead = new List<Dictionary<string, object>>();
    private void Awake()
    {
        panel = FindObjectOfType<panelOnoff>();
        csvUpdate = FindObjectOfType<csvUpdatePanel>();
        goldRead = CSVReader.Read("ShopGoldData");
        //GoldButton.onClick.AddListener(Process);
    }



    private void Start()
    {
        ArrNumber = int.Parse(transform.name);
        panel_Load();
    }

    public void Process()
    {
        ReadData?.Invoke();
    }

    public void DataUpdate()
    {
        if (goldRead.Count-1 <= LvCount) return;

        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;
        LvCount++;

        GoldButtonText.text = UserMoney();
        ability2.text = "+" + goldRead[LvCount][ArrNumber + "번능력치"].ToString();
        UserDataMansger.Instance.LvDataList[ArrNumber].LvCount = LvCount;
    }

    public string UserMoney()
    {
        var Current_Money = UserDataMansger.Instance.userData.Money;
        if (goldRead[LvCount][ArrNumber + "번금액"].ToString() != "MAX")
        {
            Current_Money -= int.Parse(goldRead[LvCount - 1][ArrNumber + "번금액"].ToString());
            UserDataMansger.Instance.userData.Money = Current_Money;
        }
        else
        {
            Current_Money -= int.Parse(goldRead[LvCount - 1][ArrNumber + "번금액"].ToString());
            UserDataMansger.Instance.userData.Money = Current_Money;
        }

        return goldRead[LvCount][ArrNumber + "번금액"].ToString();
    }



    public void panel_Load()
    {
       
        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;

        if (goldRead.Count-1 <= LvCount)
        {
            GoldButtonText.text = "Max";
            ability2.text = "+" + goldRead[LvCount][ArrNumber + "번능력치"].ToString();
        }
        else
        {
            GoldButtonText.text = goldRead[LvCount][ArrNumber + "번금액"].ToString();
            ability2.text = "+" + goldRead[LvCount][ArrNumber + "번능력치"].ToString();
        }

    }





    public void DataEquip()
    {
        Debug.Log("장착?능력?");
        //1.장착시 csv에 있는 아이템능력치를 유저데이타에 연동?
        //2.구입?시 csv에 있는 아이템능력치를 가져와 아이템생성?
        //3.활성화되면 csv에 있는 아이템 능력치를 가져와 유저데이타에 연동?




    }

    public void DataBm()
    {
        Debug.Log("??");
    }

}