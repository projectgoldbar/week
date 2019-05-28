using System;
using System.Collections;
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

    public string Key = null;

    public UnityEvent LoadData;
    private panelOnoff panel;
    private csvUpdatePanel csvUpdate;
    public List<Dictionary<string, object>> goldRead = new List<Dictionary<string, object>>();


    private void Awake()
    {
        panel = FindObjectOfType<panelOnoff>();
        csvUpdate = FindObjectOfType<csvUpdatePanel>();
        goldRead = CSVReader.Read("UpdateDate");
        
        //GoldButton.onClick.AddListener(Process);
    }


    private void OnEnable()
    {
        if (panel.panelName == PanelName.Upgrade)
        {
            ArrNumber = int.Parse(transform.name);
            Update_Load();
        }
    }

   
    public void DataUpdate()
    {
        var Current_Money = UserDataMansger.Instance.Money;
        
        //if (goldRead.Count-1 <= LvCount || 
        if(goldRead[LvCount][ArrNumber + "_money"].ToString() == "MAX" ||
            ((Current_Money -= int.Parse(goldRead[LvCount][ArrNumber + "_money"].ToString())) <0))
            return;

       
        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;

        GoldButtonText.text = UserMoney();
        


        var vv = float.Parse(goldRead[LvCount][ArrNumber + "_value"].ToString());
        ability2.text = vv.ToString();
        UserDataMansger.Instance.LvDataList[ArrNumber].LvCount = LvCount;
        UserDataMansger.Instance.updateData[14-ArrNumber] = DATA();

        PlayerDataSetup();

    }



    public static void PlayerDataSetup(float addhp =0)
    {


        UserAbillity Abillity = new UserAbillity();

        //최대체력
        //체력수치 * (세트효율+100)*0.01
        Abillity.MaxHp = UserDataMansger.Instance.updateData[14].Data+ addhp;

        //체력감소속도
        Abillity.Hpdeceleration = UserDataMansger.Instance.updateData[13].Data;
        //방어력
        Abillity.DEF = UserDataMansger.Instance.updateData[12].Data;
        //획득 체력
        Abillity.HpGain = UserDataMansger.Instance.updateData[11].Data;
        //획득 진화포인트
        Abillity.Gainevolution = UserDataMansger.Instance.updateData[10].Data;
        //골드 획득량
        Abillity.MoneyGain = UserDataMansger.Instance.updateData[9].Data;
        //시작거리
        Abillity.StartRange = UserDataMansger.Instance.updateData[8].Data;
       

        UserDataMansger.Instance.userData.userAbillity = Abillity;
    }

    public UpdateData DATA()
    {
        UpdateData data = new UpdateData();
        data.Index = ArrNumber;
        data.Name = Name.text;
        data.Data = Convert.ToSingle(ability2.text);

        data.OBJ = this;

        return data;
    }


    public string UserMoney()
    {
        var Current_Money = UserDataMansger.Instance.userData.Money;

        if (Current_Money >= 0)
        {
            LvCount++;

            Current_Money -= int.Parse(goldRead[LvCount - 1][ArrNumber + "_money"].ToString());
            UserDataMansger.Instance.userData.Money = Current_Money;
            UserDataMansger.Instance.Money = UserDataMansger.Instance.userData.Money;
        }

        return goldRead[LvCount][ArrNumber + "_money"].ToString();
    }

    //유니티이벤트로 넣음 
    public void Update_Load()
    {
        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;

        GoldButtonText.text = goldRead[LvCount][ArrNumber + "_money"].ToString();
        ability2.text = goldRead[LvCount][ArrNumber + "_value"].ToString();

        UserDataMansger.Instance.updateData[ArrNumber] = DATA();
    }



}