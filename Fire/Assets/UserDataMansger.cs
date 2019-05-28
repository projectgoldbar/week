using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;

public class UserDataMansger : Singleton<UserDataMansger>
{
    public List<LvUpData> LvDataList = new List<LvUpData>();

    public UserData userData;

    public List<UpdateData> updateData = new List<UpdateData>();

    public List<ChildReference1> equipData = new List<ChildReference1>();


    [System.NonSerialized]
    public string userdataname = "USERDATA.dat";
    [System.NonSerialized]
    public string LvupgradeDataname = "LVUpData.dat";
    [System.NonSerialized]
    public string EquipDataName = "EquipData.dat";

    public bool b_file = false;

    public List<Dictionary<string, object>> CsvReadUpgradeData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> CsvReadUpgradeGoldData = new List<Dictionary<string, object>>();

    public List<Dictionary<string, object>> CsvReadEquipData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> CsvReadEquipDataSet = new List<Dictionary<string, object>>();


    private CsvEquipPanel csvEquip;

    public TextMeshProUGUI UserMoney = null;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            if(UserMoney == null)
            {
                UserMoney = FindObjectOfType<TextMeshMoney>().GetComponent<TextMeshProUGUI>();
            }
            UserMoney.text = money.ToString(); 
        }
    }
    private int money;
    // Start is called before the first frame update
    private void Awake()
    {
        csvEquip = FindObjectOfType<CsvEquipPanel>();


        //System.Array.Clear(userData.skillLVList, 0, 23);


        DataBinaryLoad(userdataname);
        LVUPGRADEDATALoad(LvupgradeDataname);
        

        Money = userData.Money;

        

        CsvReadUpgradeData
           = CSVReader.Read("UpgradeTextData");
        CsvReadUpgradeGoldData
            = CSVReader.Read("UpdateDate");
        CsvReadEquipData
            = CSVReader.Read("EquipTextData");
        CsvReadEquipDataSet
            = CSVReader.Read("EquipTextData2");

    }


    #region CSV로드
    public void UserCSVDataRead()
    {
        var data = CSVReader.Read("UserDataCSV");
        UserData user = new UserData();

        //user.hpLV                       = CSVIntParseData(data, "hpLV");
        //user.clearBonusDNALV            = CSVIntParseData(data, "clearBonusDNALV");
        //user.DNAStorageLV               = CSVIntParseData(data, "DNAStorageLV");
        //user.ZDNAStorageLV              = CSVIntParseData(data, "ZDNAStorageLV");
        //user.bootyLV                    = CSVIntParseData(data, "bootyLV");

        userData = user;

        ScriptableObject scriptable = new ScriptableObject();
        
    }
    public int CSVIntParseData(List<Dictionary<string, object>> data, string KeyValue)
    {
        return int.Parse(data[0][KeyValue].ToString());
    }
    #endregion

    

    private void OnApplicationQuit()
    {
        LVUPGRADEDATASave(LvupgradeDataname);
        UserDataBinarySave(userdataname);
    }



    //바이너리 저장
    public void UserDataBinarySave(string DATANAME)
    {
        BinaryFormatter binaryf = new BinaryFormatter();
        FileStream file = File.Open(getPath(DATANAME),FileMode.Open);

        //데이터 셋팅
        UserData user = UserDataSetting();
        UserDataSetting(user);

        binaryf.Serialize(file, user);
        file.Close();
    }


    //바이너리 로드
    //파일이 있으면 로드
    //파일이 없으면 빈파일생성
    public void DataBinaryLoad(string DATANAME)
    {
        BinaryFormatter binaryf = new BinaryFormatter();
        FileStream file = File.Open(getPath(DATANAME),FileMode.OpenOrCreate);

        if (file != null && file.Length > 0)
        {
            UserData tempData = (UserData)binaryf.Deserialize(file);

            userData = tempData;
        }
        file.Close();
    }


    public void LVUPGRADEDATASave(string DATANAME)
    {
        BinaryFormatter binaryf = new BinaryFormatter();
        FileStream file = File.Open(getPath(DATANAME), FileMode.Open);

        //데이터 셋팅
        List<LvUpData> tempList = LvDataList;

        binaryf.Serialize(file, tempList);
        file.Close();
    }

   
   

   

    public void LVUPGRADEDATALoad(string DATANAME)
    {
        BinaryFormatter binaryf = new BinaryFormatter();
        FileStream file = File.Open(getPath(DATANAME), FileMode.OpenOrCreate);

        if (file != null && file.Length > 0)
        {
            List<LvUpData> tempList = (List<LvUpData>)binaryf.Deserialize(file);
            LvDataList = tempList;
        }
        file.Close();
    }

   


    //실제 사용될 데이터들을 셋팅
    public UserData UserDataSetting()
    {
        UserData user = new UserData();

        //실제 사용될 데이터의 변수들 셋팅

        user.userAbillity                   = userData.userAbillity;

        user.skillLVList                    = userData.skillLVList;
        user.skillEquip                     = userData.skillEquip;
        user.skillCollection                = userData.skillCollection;

        user.Money                          = userData.Money;

        return user;
    }

    
    public void UserDataSetting(UserData USER)
    {
        userData = USER;
    }

    private string getPath(string FileName)
    {
#if UNITY_EDITOR
        return Application.dataPath +"/" + FileName;
#elif UNITY_ANDROID
        return Application.persistentDataPath+"/" +FileName;
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+FileName;
#else
        return Application.dataPath +"/"+FileName;
#endif
    }


    //아이템(스킬?) 수집
    public void CollectionSkill(int v)   //v는 아이템의 인덱스번호
    {
        LobyDataManager.Instance.reference1[v].b_Collection = true;
        CsvEquipPanel.CollectionPanelOnoff();
        csvEquip.Seteffect();
    }

}

[System.Serializable]
public class LvUpData
{
    public int LvCount;
}

[System.Serializable]
public class UpdateData
{
    public int Index;
    public string Name;
    public float Data;

    public ChildReference OBJ;
}

[System.Serializable]
public class EquipDataSet
{
    public string name;
    public int Key;
    public int Data;
    

    public EquipDataSet() { }
    public EquipDataSet(string name,int data)
    {
        this.name = name;
        this.Data = data;
    }


}
[System.Serializable]
public class Equipdata
{
    public int Index;
    public string Name;
    public bool b_Panel;
    public bool b_collection;
    public float Addhp;
    public EquipRefData equipRef;
    public List<EquipDataSet> DataList;
    public ChildReference1 child;
    public Equipdata() { }
    public Equipdata(int index,
                     string name,
                     bool b_Panel,
                    float Addhp,
                     List<EquipDataSet> DataList)
    {
        this.Index = index;
        this.Name = name;
        this.b_Panel = b_Panel;
        this.Addhp = Addhp;
        this.DataList = DataList;
    }

}
