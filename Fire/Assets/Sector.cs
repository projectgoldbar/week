using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    private SectorManager sectorManager;
    public int sectorNumber = 0;
    public int maxCoin = 10;

    public int[] spwanSectorNumber;

    private void Awake()
    {
        sectorManager = FindObjectOfType<SectorManager>();
    }

    public bool OnPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        OnPlayer = true;
        for (int i = 0; i < sectorManager.isPlayerOnList.Count; i++)
        {
            sectorManager.isPlayerOnList[i] = false;
        }
        sectorManager.isPlayerOnList[sectorNumber] = this.OnPlayer;
        for (int i = 0; i < 10; i++)
        {
            sectorManager.coinSpwaner.SpwanCoin(spwanSectorNumber);
        }
    }
}