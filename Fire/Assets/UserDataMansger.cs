using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class UserDataMansger : Singleton<UserDataMansger>
{
    public List<LvUpData> LvDataList = new List<LvUpData>();

    public UserData userData;

    string userdataname = "USERDATA.dat";
    string LvupgradeDataname = "LVUpData.dat";

    // Start is called before the first frame update
    private void Awake()
    {
        DataBinaryLoad(userdataname);
        LVUPGRADEDATALoad(LvupgradeDataname);
    }


    #region CSV로드
    public void UserCSVDataRead()
    {
        var data = CSVReader.Read("UserDataCSV");
        UserData user = new UserData();

        user.hpLV                       = CSVIntParseData(data, "hpLV");
        user.clearBonusDNALV            = CSVIntParseData(data, "clearBonusDNALV");
        user.DNAStorageLV               = CSVIntParseData(data, "DNAStorageLV");
        user.ZDNAStorageLV              = CSVIntParseData(data, "ZDNAStorageLV");
        user.bootyLV                    = CSVIntParseData(data, "bootyLV");

        userData = user;

        ScriptableObject scriptable = new ScriptableObject();
        
    }
    public int CSVIntParseData(List<Dictionary<string, object>> data, string KeyValue)
    {
        return int.Parse(data[0][KeyValue].ToString());
    }
    #endregion



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LVUPGRADEDATASave(LvupgradeDataname);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UserDataBinarySave(userdataname);
        }
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


            Debug.Log("lv");
            foreach (var item in LvDataList)
            {
                Debug.Log(item.LvCount);
            }
        }
        file.Close();
    }




    //실제 사용될 데이터들을 셋팅
    public UserData UserDataSetting()
    {
        UserData user = new UserData();

        //실제 사용될 데이터의 변수들 셋팅
        user.hpLV = 1;
        user.clearBonusDNALV = 1;
        user.DNAStorageLV = 1;
        user.ZDNAStorageLV = 1;
        user.bootyLV = 1;
        user.Money = 1000;

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

}

[System.Serializable]
public class LvUpData
{
    public int LvCount;
}