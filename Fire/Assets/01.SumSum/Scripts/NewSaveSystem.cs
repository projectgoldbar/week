using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class NewSaveSystem
{
    public static void SaveData(NewUserData userData, float RGold)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/alpa.dd";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData();
        data.statPointerIdx = userData.statPointerIdx;
        data.playTime = userData.playTime;
        data.money = RGold;
        data.gainSkin = userData.gainSkin;
        data.achivements = userData.achievements;
        data.equipedSkinIdx = userData.equipedSkinIdx;
        data.bronzeBox = userData.bronzeBoxCount;
        data.silberBox = userData.silverBoxCount;
        data.goldBox = userData.goldBoxCount;
        data.adoff = userData.AdOff;
        data.goldBouns = userData.goldBonus;
        data.pakage = userData.pakage;
        data.isTutorialClear = userData.isTutorialClear;
        data.highScore = userData.highScore;
        data.accumulateBoxCount = userData.accumulateBoxCount;
        data.accumulateBoxOpen = userData.accumulateBoxOpen;
        data.accumulateHealPack = userData.accumulateHealPack;
        data.playCount = userData.playCount;
        data.highStage = userData.highStage;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/alpa.dd";
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