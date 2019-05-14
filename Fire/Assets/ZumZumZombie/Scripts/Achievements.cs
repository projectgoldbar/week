using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public List<Dictionary<string, object>> data;

    private void Start()
    {
        data = CSVReader.Read("test");
        for (int i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i]["No"] + "" + data[i]["Clear"] + "" + data[i]["Result"]);
        }
    }
}