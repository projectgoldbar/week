using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    public Text currLVTxt;

    public Text _statIndexTxt;
    public Text _priceTxt;
    public Text _valueTxt;
    private int maxMoney = 999999;
    private int currMoney;
    private int currLV = 1;
    private int _statIndex;
    private int _price;
    private int _value;

    private Upgrade00maxHpCSV upgrade00maxHpCSV;

    private void Start()
    {
    }

    public void CheckSearch()
    {
        currLV = Convert.ToInt32(currLVTxt.text);

        upgrade00maxHpCSV = CSVManager.Instance.GetUpgrade00maxHpCSV(currLV);
        _statIndex = upgrade00maxHpCSV.statIndex;
        _price = upgrade00maxHpCSV.price;
        _value = upgrade00maxHpCSV.value;

        Refresh();
    }

    public void Refresh()
    {
        _statIndexTxt.text = string.Format("_statIndex : {0}", _statIndex);
        _priceTxt.text = string.Format("_price : {0}", _price);
        _valueTxt.text = string.Format("_value : {0}", _value);
    }
}