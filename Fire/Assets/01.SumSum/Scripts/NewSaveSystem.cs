using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class NewSaveSystem
{
    public static void SaveData(NewUserData userData, float RGold)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/beta.dd";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData();
        data.statPointerIdx = userData.statPointerIdx;
        data.playTime = userData.playTime;
        data.money = RGold;
        data.gainSkin = userData.gainSkin;
        data.equipedSkinIdx = userData.equipedSkinIdx;
        data.bronzeBox = userData.bronzeBoxCount;
        data.silberBox = userData.silverBoxCount;
        data.goldBox = userData.goldBoxCount;
        data.adoff = userData.AdOff;
        data.isTutorialClear = userData.isTutorialClear;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/beta.dd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            SaveData data = new SaveData();
            Debug.LogError("Save file not found in" + path);
            return data;
        }
    }
}