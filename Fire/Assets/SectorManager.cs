using System.Collections.Generic;
using UnityEngine;

public class SectorManager : MonoBehaviour
{
    public List<Bounds> bounds;
    public GameObject SectorGroupParent;
    public CoinSpwaner coinSpwaner;
    public List<bool> isPlayerOnList;

    private void Awake()
    {
        coinSpwaner = FindObjectOfType<CoinSpwaner>();
        var x = SectorGroupParent.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < x.Length; i++)
        {
            bounds.Add(x[i].bounds);
            isPlayerOnList.Add(x[i].GetComponent<Sector>().OnPlayer);
        }
    }
}