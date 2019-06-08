using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public GameObject coin;
    public List<GameObject> coinPool;

    private void Awake()
    {
        for (int i = 0; i < 80; i++)
        {
            coinPool.Add(Instantiate(coin, this.transform.position, Quaternion.identity));
        }
    }
}