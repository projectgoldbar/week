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

    public UnityEvent LoadData;
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
        if (panel.panelName == PanelName.Upgrade)
        {
            ArrNumber = int.Parse(transform.name);
            Update_Load();
        }
    }

    public void DataUpdate()
    {
        var Current_Money = UserDataMansger.Instance.Money;
        
        if (goldRead.Count-1 <= LvCount || 
            ((Current_Money -= int.Parse(goldRead[LvCount][ArrNumber + "번금액"].ToString())) <0))
            return;

        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;

        GoldButtonText.text = UserMoney();
        ability2.text = goldRead[LvCount][ArrNumber + "번능력치"].ToString();
        UserDataMansger.Instance.LvDataList[ArrNumber].LvCount = LvCount;

        UserDataMansger.Instance.updateData[ArrNumber] = DATA();

        PlayerDataSetup();


    }

    public void PlayerDataSetup()
    {
        UserAbillity Abillity = new UserAbillity(){
            //최대체력
            _MaxHp               = UserDataMansger.Instance.updateData[0].Data,
            //체력감소속도
            Hpdeceleration      = UserDataMansger.Instance.updateData[1].Data,
            //방어력
            DEF                 = UserDataMansger.Instance.updateData[2].Data,
            //획득 체력
            HpGain              = UserDataMansger.Instance.updateData[3].Data,
            //획득 진화포인트
            Gainevolution       = UserDataMansger.Instance.updateData[4].Data,
            //골드 획득량
            MoneyGain           = UserDataMansger.Instance.updateData[5].Data,
            //시작거리
            StartRange          = UserDataMansger.Instance.updateData[6].Data
        };

        UserDataMansger.Instance.userData.userAbillity = Abillity;


    }


    public UpdateData DATA()
    {
        UpdateData data = new UpdateData();
        data.Index = ArrNumber;
        data.Name = Name.text;
        data.Data = int.Parse(ability2.text);

        return data;
    }


    public string UserMoney()
    {
        var Current_Money = UserDataMansger.Instance.userData.Money;

        if (Current_Money >= 0)
        {
            LvCount++;

            Current_Money -= int.Parse(goldRead[LvCount - 1][ArrNumber + "번금액"].ToString());
            UserDataMansger.Instance.userData.Money = Current_Money;
            UserDataMansger.Instance.Money = UserDataMansger.Instance.userData.Money;
        }

        return goldRead[LvCount][ArrNumber + "번금액"].ToString();
    }

    //유니티이벤트로 넣음 
    public void Update_Load()
    {
        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;
        
        GoldButtonText.text = goldRead[LvCount][ArrNumber + "번금액"].ToString();
        ability2.text = goldRead[LvCount][ArrNumber + "번능력치"].ToString();

        UserDataMansger.Instance.updateData.Add(DATA());

        UserDataMansger.Instance.udateDataDic.Add(Name.text, DATA());


       



    }


   



    public void DataEquip()
    {
        // Debug.Log("장착과 능력");
        //1.장착시 csv에 있는 아이템능력치를 유저데이타에 연동?
        //2.구입?시 csv에 있는 아이템능력치를 가져와 아이템생성?
        //3.활성화되면 csv에 있는 아이템 능력치를 가져와 유저데이타에 연동?




    }

    public void DataBm()
    {
        Debug.Log("??");
    }

}