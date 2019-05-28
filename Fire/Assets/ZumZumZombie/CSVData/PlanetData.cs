using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{
    //public TextAsset data;

    public List<Dictionary<string, object>> dataSet = new List<Dictionary<string, object>>();

    private void Start()
    {
        dataSet = CSVReader.Read("SaveData");

        Debug.Log(dataSet[0]["qwq"]);
    }
}