using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class dataInfo
{
    public string PlanetName;
    public string QuestContent;

    public dataInfo()
    {
    }

    public dataInfo(string PlanetName, string QuestContent)
    {
        this.PlanetName = PlanetName;
        this.QuestContent = QuestContent;
    }
}

public class QuestDateBase : MonoBehaviour
{
    private Test planetsArray;

    private void Start()
    {
        //planetsArray = GetComponent<Test>();
        //QuestSetup();
    }

    //csv로 만들어보기 전에 코드로 만듬
    public Dictionary<string, dataInfo> database = new Dictionary<string, dataInfo>();

    //private void QuestSetup()
    //{
    //    database.Add(planetsArray.pranets[0].name, new dataInfo()
    //    {
    //        PlanetName = "Mercury",
    //        QuestContent = "퀘스트1"
    //    });
    //    database.Add(planetsArray.pranets[1].name, new dataInfo()
    //    {
    //        PlanetName = "Venus",
    //        QuestContent = "퀘스트2"
    //    });
    //    database.Add(planetsArray.pranets[2].name, new dataInfo()
    //    {
    //        PlanetName = "Mars",
    //        QuestContent = "퀘스트3"
    //    });
    //    database.Add(planetsArray.pranets[3].name, new dataInfo()
    //    {
    //        PlanetName = "Jupiter",
    //        QuestContent = "퀘스트4"
    //    });
    //    database.Add(planetsArray.pranets[4].name, new dataInfo()
    //    {
    //        PlanetName = "Saturn",
    //        QuestContent = "퀘스트5"
    //    });
    //}
}