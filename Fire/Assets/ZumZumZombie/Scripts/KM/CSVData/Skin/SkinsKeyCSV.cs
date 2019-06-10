using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsKeyCSV
{
    private int m_skinID;
    private int m_openLV;
    private int m_skinIndex;
    private int m_skinSetIndex;
    private int m_skinSetIndexCount;
    private int m_statIndex;
    private int m_statBonusValue;
    private int m_helpCounts;
    private int m_helpIndex_1;
    private int m_helpLV_1;
    private int m_helpIndex_2;
    private int m_helpLV_2;
    private int m_helpIndex_3;
    private int m_helpLV_3;

    public int skinID { get { return m_skinID; } set { m_skinID = value; } }
    public int openLV { get { return m_openLV; } set { m_openLV = value; } }
    public int skinIndex { get { return m_skinIndex; } set { m_skinIndex = value; } }
    public int skinSetIndex { get { return m_skinSetIndex; } set { m_skinSetIndex = value; } }
    public int skinSetIndexCount { get { return m_skinSetIndexCount; } set { m_skinSetIndexCount = value; } }
    public int statIndex { get { return m_statIndex; } set { m_statIndex = value; } }
    public int statBonusValue { get { return m_statBonusValue; } set { m_statBonusValue = value; } }
    public int helpCounts { get { return m_helpCounts; } set { m_helpCounts = value; } }
    public int helpIndex_1 { get { return m_helpIndex_1; } set { m_helpIndex_1 = value; } }
    public int helpLV_1 { get { return m_helpLV_1; } set { m_helpLV_1 = value; } }
    public int helpIndex_2 { get { return m_helpIndex_2; } set { m_helpIndex_2 = value; } }
    public int helpLV_2 { get { return m_helpLV_2; } set { m_helpLV_2 = value; } }
    public int helpIndex_3 { get { return m_helpIndex_3; } set { m_helpIndex_3 = value; } }
    public int helpLV_3 { get { return m_helpLV_3; } set { m_helpLV_3 = value; } }

    public void Parse(string[] values, int index, SkinsKeyCSV defaultValue)
    {
        int arrayIndex = 0;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_skinID = defaultValue.skinID;
        else
            m_skinID = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_openLV = defaultValue.openLV;
        else
            m_openLV = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_skinIndex = defaultValue.skinIndex;
        else
            m_skinIndex = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_skinSetIndex = defaultValue.skinSetIndex;
        else
            m_skinSetIndex = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_skinSetIndexCount = defaultValue.skinSetIndexCount;
        else
            m_skinSetIndexCount = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_statIndex = defaultValue.statIndex;
        else
            m_statIndex = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_statBonusValue = defaultValue.statBonusValue;
        else
            m_statBonusValue = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_helpCounts = defaultValue.helpCounts;
        else
            m_helpCounts = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_helpIndex_1 = defaultValue.helpIndex_1;
        else
            m_helpIndex_1 = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_helpLV_1 = defaultValue.helpLV_1;
        else
            m_helpLV_1 = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_helpIndex_2 = defaultValue.helpIndex_2;
        else
            m_helpIndex_2 = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_helpLV_2 = defaultValue.helpLV_2;
        else
            m_helpLV_2 = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_helpIndex_3 = defaultValue.helpIndex_3;
        else
            m_helpIndex_3 = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;
        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_helpLV_3 = defaultValue.helpLV_3;
        else
            m_helpLV_3 = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;
    }

    static public void LoadCSV(string fileName, List<SkinsKeyCSV> list)
    {
        TextAsset txtFile = Resources.Load(fileName) as TextAsset;
        string fullpath = txtFile.text;
        string[] values = fullpath.Split(',', '\n');
        for (int i = 0; i < values.Length; ++i)
        {
            values[i] = values[i].Replace("\r", "");
            values[i] = values[i].Replace("\u00a0", " ");
        }

        int numColumn = int.Parse(values[0]);

        if (numColumn == 0)
        {
            Debug.Log("numColumn is 0");
            return;
        }

        SkinsKeyCSV infoCurr;
        SkinsKeyCSV infoDefault = new SkinsKeyCSV();

        int numRow = (values.Length - 1) / numColumn;
        for (int i = 0; i < numRow; ++i)
        {
            if (i < 3)
                continue;

            if (i == 3)
            {
                // Default 값 세팅.
                infoDefault.Parse(values, i * numColumn, null);
            }
            else
            {
                infoCurr = new SkinsKeyCSV();
                infoCurr.Parse(values, i * numColumn, infoDefault);
                list.Add(infoCurr);
            }
        }
    }
}