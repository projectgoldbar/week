using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    private static GameInfo instance;
    public GameDataKM m_gameData;
    private InvenDataKM m_invenData = new InvenDataKM(); //GameInfo.cs 는 앞으로도 여러 가지 저장하게 될 데이터를 관리

    public static GameInfo Instance
    {
        get { return instance; }
    }

    public GameDataKM gameData
    {
        get { return m_gameData; }
    }

    public InvenDataKM invenData
    {
        get { return m_invenData; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void FirstSetting()
    {
        m_gameData.FirstSetting();
        m_invenData.FirstSetting();
    }
}