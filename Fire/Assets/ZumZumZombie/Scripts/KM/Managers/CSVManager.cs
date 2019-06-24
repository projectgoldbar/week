using System.Collections.Generic;
using UnityEngine;

public class CSVManager : MonoBehaviour
{
    private static CSVManager instance;

    private List<Upgrade00maxHpCSV> m_Upgrade00maxHpCSVList = new List<Upgrade00maxHpCSV>();
    private List<Upgrade01decelerationHpCSV> m_Upgrade01decelerationHpCSVList = new List<Upgrade01decelerationHpCSV>();
    private List<Upgrade02defenseCSV> m_Upgrade02defenseCSVList = new List<Upgrade02defenseCSV>();
    private List<Upgrade03gainHpCSV> m_Upgrade03gainHpCSVList = new List<Upgrade03gainHpCSV>();
    private List<Upgrade04gainPointCSV> m_Upgrade04gainPointCSVList = new List<Upgrade04gainPointCSV>();
    private List<Upgrade05gainMoneyCSV> m_Upgrade05gainMoneyCSVList = new List<Upgrade05gainMoneyCSV>();
    private List<Upgrade06startBonusCSV> m_Upgrade06startBonusCSVList = new List<Upgrade06startBonusCSV>();
    private List<Upgrade07skillBodyRatioCSV> m_Upgrade07skillBodyRatioCSVList = new List<Upgrade07skillBodyRatioCSV>();
    private List<Upgrade08skillDurationCSV> m_Upgrade08skillDurationCSVList = new List<Upgrade08skillDurationCSV>();
    private List<Upgrade09skillMaxCountCSV> m_Upgrade09skillMaxCountCSVList = new List<Upgrade09skillMaxCountCSV>();
    private List<Upgrade10instantStartBonusCSV> m_Upgrade10instantStartBonusCSVList = new List<Upgrade10instantStartBonusCSV>();
    private List<Upgrade11instantGainMoneyCSV> m_Upgrade11instantGainMoneyCSVList = new List<Upgrade11instantGainMoneyCSV>();
    private List<Upgrade12instantGainMaxHpCSV> m_Upgrade12instantGainMaxHpCSVList = new List<Upgrade12instantGainMaxHpCSV>();
    private List<Upgrade13instantSkillMaxCountCSV> m_Upgrade13instantSkillMaxCountCSVList = new List<Upgrade13instantSkillMaxCountCSV>();
    private List<Upgrade14instantGainHpCSV> m_Upgrade14instantGainHpCSVList = new List<Upgrade14instantGainHpCSV>();

    public static CSVManager Instance
    {
        set { instance = value; }
        get { return instance; }
    }

    public void Awake()
    {
        instance = this;

        Upgrade00maxHpCSV.LoadCSV("Upgrade00maxHpCSV", m_Upgrade00maxHpCSVList);
        Upgrade01decelerationHpCSV.LoadCSV("Upgrade01decelerationHpCSV", m_Upgrade01decelerationHpCSVList);
        Upgrade02defenseCSV.LoadCSV("Upgrade02defenseCSV", m_Upgrade02defenseCSVList);
        Upgrade03gainHpCSV.LoadCSV("Upgrade03gainHpCSV", m_Upgrade03gainHpCSVList);
        Upgrade04gainPointCSV.LoadCSV("Upgrade04gainPointCSV", m_Upgrade04gainPointCSVList);
        Upgrade05gainMoneyCSV.LoadCSV("Upgrade05gainMoneyCSV", m_Upgrade05gainMoneyCSVList);
        Upgrade06startBonusCSV.LoadCSV("Upgrade06startBonusCSV", m_Upgrade06startBonusCSVList);
        Upgrade07skillBodyRatioCSV.LoadCSV("Upgrade07skillBodyRatioCSV", m_Upgrade07skillBodyRatioCSVList);
        Upgrade08skillDurationCSV.LoadCSV("Upgrade08skillDurationCSV", m_Upgrade08skillDurationCSVList);
        Upgrade09skillMaxCountCSV.LoadCSV("Upgrade09skillMaxCountCSV", m_Upgrade09skillMaxCountCSVList);
        Upgrade10instantStartBonusCSV.LoadCSV("Upgrade10instantStartBonusCSV", m_Upgrade10instantStartBonusCSVList);
        Upgrade11instantGainMoneyCSV.LoadCSV("Upgrade11instantGainMoneyCSV", m_Upgrade11instantGainMoneyCSVList);
        Upgrade12instantGainMaxHpCSV.LoadCSV("Upgrade12instantGainMaxHpCSV", m_Upgrade12instantGainMaxHpCSVList);
        Upgrade13instantSkillMaxCountCSV.LoadCSV("Upgrade13instantSkillMaxCountCSV", m_Upgrade13instantSkillMaxCountCSVList);
        Upgrade14instantGainHpCSV.LoadCSV("Upgrade14instantGainHpCSV", m_Upgrade14instantGainHpCSVList);
    }

    public Upgrade00maxHpCSV GetUpgrade00maxHpCSV(int level)
    {
        if (m_Upgrade00maxHpCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgradeMaxHpCSV! level={0}", level));
            return null;
        }
        return m_Upgrade00maxHpCSVList[level - 1];
    }

    public Upgrade01decelerationHpCSV GetUpgrade01decelerationHpCSV(int level)
    {
        if (m_Upgrade01decelerationHpCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade01decelerationHpCSV! level={0}", level));
            return null;
        }
        return m_Upgrade01decelerationHpCSVList[level - 1];
    }

    public Upgrade02defenseCSV GetUpgrade02defenseCSV(int level)
    {
        if (m_Upgrade02defenseCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade02defenseCSV! level={0}", level));
            return null;
        }
        return m_Upgrade02defenseCSVList[level - 1];
    }

    public Upgrade03gainHpCSV GetUpgrade03gainHpCSV(int level)
    {
        if (m_Upgrade03gainHpCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade03gainHpCSV! level={0}", level));
            return null;
        }
        return m_Upgrade03gainHpCSVList[level - 1];
    }

    public Upgrade04gainPointCSV GetUpgrade04gainPointCSV(int level)
    {
        if (m_Upgrade04gainPointCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade04gainPointCSV! level={0}", level));
            return null;
        }
        return m_Upgrade04gainPointCSVList[level - 1];
    }

    public Upgrade05gainMoneyCSV GetUpgrade05gainMoneyCSV(int level)
    {
        if (m_Upgrade05gainMoneyCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade05gainMoneyCSV! level={0}", level));
            return null;
        }
        return m_Upgrade05gainMoneyCSVList[level - 1];
    }

    public Upgrade06startBonusCSV GetUpgrade06startBonusCSV(int level)
    {
        if (m_Upgrade06startBonusCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade06startBonusCSV! level={0}", level));
            return null;
        }
        return m_Upgrade06startBonusCSVList[level - 1];
    }

    public Upgrade07skillBodyRatioCSV GetUpgrade07skillBodyRatioCSV(int level)
    {
        if (m_Upgrade07skillBodyRatioCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade07skillBodyRatioCSV! level={0}", level));
            return null;
        }
        return m_Upgrade07skillBodyRatioCSVList[level - 1];
    }

    public Upgrade08skillDurationCSV GetUpgrade08skillDurationCSV(int level)
    {
        if (m_Upgrade08skillDurationCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade08skillDurationCSV! level={0}", level));
            return null;
        }
        return m_Upgrade08skillDurationCSVList[level - 1];
    }

    public Upgrade09skillMaxCountCSV GetUpgrade09skillMaxCountCSV(int level)
    {
        if (m_Upgrade09skillMaxCountCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade09skillMaxCountCSV! level={0}", level));
            return null;
        }
        return m_Upgrade09skillMaxCountCSVList[level - 1];
    }

    public Upgrade10instantStartBonusCSV GetUpgrade10instantStartBonusCSV(int level)
    {
        if (m_Upgrade10instantStartBonusCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade10instantStartBonusCSV! level={0}", level));
            return null;
        }
        return m_Upgrade10instantStartBonusCSVList[level - 1];
    }

    public Upgrade11instantGainMoneyCSV GetUpgrade11instantGainMoneyCSV(int level)
    {
        if (m_Upgrade11instantGainMoneyCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade11instantGainMoneyCSV! level={0}", level));
            return null;
        }
        return m_Upgrade11instantGainMoneyCSVList[level - 1];
    }

    public Upgrade12instantGainMaxHpCSV GetUpgrade12instantGainMaxHpCSV(int level)
    {
        if (m_Upgrade12instantGainMaxHpCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade12instantGainMaxHpCSV! level={0}", level));
            return null;
        }
        return m_Upgrade12instantGainMaxHpCSVList[level - 1];
    }

    public Upgrade13instantSkillMaxCountCSV GetUpgrade13instantSkillMaxCountCSV(int level)
    {
        if (m_Upgrade13instantSkillMaxCountCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade13instantSkillMaxCountCSV! level={0}", level));
            return null;
        }
        return m_Upgrade13instantSkillMaxCountCSVList[level - 1];
    }

    public Upgrade14instantGainHpCSV GetUpgrade14instantGainHpCSV(int level)
    {
        if (m_Upgrade14instantGainHpCSVList.Count <= level)
        {
            Debug.LogError(string.Format("GetUpgrade14instantGainHpCSV! level={0}", level));
            return null;
        }
        return m_Upgrade14instantGainHpCSVList[level - 1];
    }
}