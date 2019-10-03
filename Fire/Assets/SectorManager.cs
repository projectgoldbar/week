using System.Collections.Generic;
using UnityEngine;

public class SectorManager : MonoBehaviour
{
    public List<Sector> sectors;
    public GameObject SectorGroupParent;
    public CoinSpwaner coinSpwaner;

    private void Awake()
    {
        //coinSpwaner = FindObjectOfType<CoinSpwaner>();

        var x = SectorGroupParent.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < x.Length; i++)
        {
            sectors.Add(x[i].GetComponent<Sector>());
        }
    }
}