using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade05gainMoneyCSV
{
    private int m_level;
    private int m_statIndex;
    private int m_price;
    private int m_value;

    public int level
    {
        get { return m_level; }
        set { m_level = value; }
    }

    public int statIndex
    {
        get { return m_statIndex; }
        set { m_statIndex = value; }
    }

    public int price
    {
        get { return m_price; }
        set { m_price = value; }
    }

    public int value
    {
        get { return m_value; }
        set { m_value = value; }
    }

    public void Parse(string[] values, int index, Upgrade05gainMoneyCSV defaultValue)
    {
        int arrayIndex = 0;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            level = defaultValue.level;
        else
            m_level = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            statIndex = defaultValue.statIndex;
        else
            m_statIndex = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_price = defaultValue.price;
        else
            m_price = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;

        if (null != defaultValue && values[index + arrayIndex].Equals(""))
            m_value = defaultValue.value;
        else
            m_value = int.Parse(values[index + arrayIndex]);
        ++arrayIndex;
    }

    static public void LoadCSV(string fileName, List<Upgrade05gainMoneyCSV> list)
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

        Upgrade05gainMoneyCSV infoCurr;
        Upgrade05gainMoneyCSV infoDefault = new Upgrade05gainMoneyCSV();

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
                infoCurr = new Upgrade05gainMoneyCSV();
                infoCurr.Parse(values, i * numColumn, infoDefault);
                list.Add(infoCurr);
            }
        }
    }
}