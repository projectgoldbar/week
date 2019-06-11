using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveDataKM
{
    private int m_maxHpLevel;

    public int maxHpLevel
    {
        get { return m_maxHpLevel; }
        set { m_maxHpLevel = value; }
    }
}

public class InvenSaveData
{
    private InvenIndex m_invenIndex;
    private int m_num;

    public InvenIndex ivenIndex
    {
        get { return m_invenIndex; }
        set { m_invenIndex = value; }
    }

    public int num
    {
        get { return m_num; }
        set { m_num = value; }
    }
}