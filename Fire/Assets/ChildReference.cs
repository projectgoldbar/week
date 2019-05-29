using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        goldRead = CSVReader.Read("UpdateDate");
        
        //GoldButton.onClick.AddListener(Process);
    }


    private void OnEnable()
    {
        
    }
    private void Start()
    {
        Update_Load();

        
    }

    public void DataUpdate()
    {
        var Current_Money = UserDataMansger.Instance.Money;
        
        if(goldRead[LvCount][ArrNumber + "_money"].ToString() == "MAX" ||
            ((Current_Money -= int.Parse(goldRead[LvCount][ArrNumber + "_money"].ToString())) <0))
            return;
       
        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;

        GoldButtonText.text = UserMoney();
        var addhp = LobyDataManager.Instance.EquipInfo.AddHp;
       
        //else addhp = 0;

        var vv = float.Parse(goldRead[LvCount][ArrNumber + "_value"].ToString())+addhp;
        ability2.text = vv.ToString();

        

        UserDataMansger.Instance.LvDataList[ArrNumber].LvCount = LvCount;
        UserDataMansger.Instance.updateData[ArrNumber] = DATA(ArrNumber);

        UpdateDataNUserData();
       // UserDataNUpdateData();

    }


    //UpdateData를 UserData로 보내줌
    public static void UpdateDataNUserData(float addhp =0,float Def =0)
    {
        UserAbillity Abillity = new UserAbillity();

        //최대체력
        //체력수치 * (세트효율+100)*0.01
        Abillity.MaxHp                  = UserDataMansger.Instance.updateData[0].Data+ addhp;

        //체력감소속도
        Abillity.Hpdeceleration         = UserDataMansger.Instance.updateData[1].Data;
        //방어력
        Abillity.DEF                    = UserDataMansger.Instance.updateData[2].Data + Def;
        //획득 체력
        Abillity.HpGain                 = UserDataMansger.Instance.updateData[3].Data;
        //획득 진화포인트
        Abillity.Gainevolution          = UserDataMansger.Instance.updateData[4].Data;
        //골드 획득량
        Abillity.MoneyGain              = UserDataMansger.Instance.updateData[5].Data;
        //2배 시간
        Abillity.StartRange             = UserDataMansger.Instance.updateData[6].Data;
        //몸의 크기
        Abillity.BodySize               = UserDataMansger.Instance.updateData[7].Data;
        //스킬 지속시간
        Abillity.duration               = UserDataMansger.Instance.updateData[8].Data;
        //스킬최대치
        Abillity.Maximum                = UserDataMansger.Instance.updateData[9].Data;
        //1회성 2배시간
        Abillity.One_Time_StartRange    = UserDataMansger.Instance.updateData[10].Data;
        //1회성 골드 획득량
        Abillity.One_Time_MoneyGain     = UserDataMansger.Instance.updateData[11].Data;
        //1회성 전체 체력증가
        Abillity.One_Time_MaxHpUp       = UserDataMansger.Instance.updateData[12].Data;
        //1회성 스킬 최대치
        Abillity.One_Time_Maximum       = UserDataMansger.Instance.updateData[13].Data;
        //1회성 획득체력
        Abillity.One_Time_HpGain        = UserDataMansger.Instance.updateData[14].Data;

        UserDataMansger.Instance.userData.userAbillity = Abillity;
    }

    //UserData를 UpdateData로 보내줌
    public static void UserDataNUpdateData(float addhp = 0, float Def = 0, Action action = null)
    {
        UserAbillity Abillity = UserDataMansger.Instance.userData.userAbillity;

        //최대체력
        //체력수치 * (세트효율+100)*0.01
        UserDataMansger.Instance.updateData[0].Data = Abillity.MaxHp + addhp;

        //체력감소속도
        UserDataMansger.Instance.updateData[1].Data = Abillity.Hpdeceleration;
        //방어력
        UserDataMansger.Instance.updateData[2].Data = Abillity.DEF + Def;
        //획득 체력
        UserDataMansger.Instance.updateData[3].Data = Abillity.HpGain;
        //획득 진화포인트
        UserDataMansger.Instance.updateData[4].Data = Abillity.Gainevolution;
        //골드 획득량
        UserDataMansger.Instance.updateData[5].Data = Abillity.MoneyGain;
        //2배 시간
        UserDataMansger.Instance.updateData[6].Data = Abillity.StartRange;
        //스킬몸의 크기
        UserDataMansger.Instance.updateData[7].Data = Abillity.BodySize;
        //스킬지속시간
        UserDataMansger.Instance.updateData[8].Data = Abillity.duration;
        //스킬최대치
        UserDataMansger.Instance.updateData[9].Data = Abillity.Maximum;
        //1회성 2배시간
        UserDataMansger.Instance.updateData[10].Data = Abillity.One_Time_StartRange;
        //1회성 골드 획득량
        UserDataMansger.Instance.updateData[11].Data = Abillity.One_Time_MoneyGain;
        //1회성 전체 체력 증가
        UserDataMansger.Instance.updateData[12].Data = Abillity.One_Time_MaxHpUp;
        //1회성 스킬최대치
        UserDataMansger.Instance.updateData[13].Data = Abillity.One_Time_Maximum;
        //1회성 획득체력
        UserDataMansger.Instance.updateData[14].Data = Abillity.One_Time_HpGain;

        action?.Invoke();
       
    }


    public static void haveFile()
    {
        for (int i = 0; i < UserDataMansger.Instance.updateData.Count; i++)
        {
            UserDataMansger.Instance.updateData[i].OBJ.ability2.text =
            UserDataMansger.Instance.updateData[i].Data.ToString();
        }

    }



    public UpdateData DATA(int arrnum =0)
    {
        UpdateData data = new UpdateData();
        data.Index = arrnum;
        data.Name = Name.text;
        data.Data = //UserDataMansger.Instance.updateData[arrnum].Data;
            float.Parse(goldRead[LvCount][arrnum + "_value"].ToString());

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
        for (int i = 0; i < UserDataMansger.Instance.userData.skillEquip.Length; i++)
        {
            if (UserDataMansger.Instance.userData.skillEquip[i])
            {
                LobyDataManager.Instance.EquipInfo = LobyDataManager.Instance.reference1[i];
            }
            else continue;
        }

        LvCount = UserDataMansger.Instance.LvDataList[ArrNumber].LvCount;

        GoldButtonText.text = goldRead[LvCount][ArrNumber + "_money"].ToString();
        //ability2.text = goldRead[LvCount][ArrNumber + "_value"].ToString();

        UserDataMansger.Instance.updateData[ArrNumber] = DATA(ArrNumber);

            ability2.text = UserDataMansger.Instance.updateData[ArrNumber].Data.ToString();


       

    }



}