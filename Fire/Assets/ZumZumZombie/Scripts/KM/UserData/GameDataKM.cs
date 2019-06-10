using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataKM
{
    private GameSaveDataKM m_saveData = new GameSaveDataKM();

    public GameSaveDataKM saveData
    {
        get { return m_saveData; }
    }

    public void MaxHpLevelUp()
    {
        ++saveData.maxHpLevel;
    }

    public void FirstSetting()
    {
        //m_saveData = LoadData();  // 데이터 로드하는것 !이 첫세팅!
        GameSaveDataKM saveData = new GameSaveDataKM();
        saveData.maxHpLevel = 1;
        SaveData(saveData);
    }

    public void SaveData(GameSaveDataKM saveData)

    {
        m_saveData = saveData;
    }
}