using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csvBMPanel : CSVdata
{

    private void Awake()
    {
        UpdataPanel_Read();
    }


    public void BMPanel_Read()
    {
        Read = CSVReader.Read(CSVFileName);
        goldData = CSVReader.Read(CSVFileGoldData);

        for (int i = 0; i < Read.Count; i++)
        {
            var obj = GameObject.Instantiate<ChildReference>(list, transform);
            obj.name = i.ToString();
            obj.Name.text = Read[i]["이름Text"].ToString();
            obj.ability.text = Read[i]["적용능력Text"].ToString() + "";
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60.0f * Read.Count);
    }

}
