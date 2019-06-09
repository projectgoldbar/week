using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public GameObject coin;
    public GameObject meat;
    public List<GameObject> coinPool;
    public List<GameObject> meatPool;

    private void Awake()
    {
        for (int i = 0; i < 80; i++)
        {
            coinPool.Add(Instantiate(coin, this.transform.position, Quaternion.identity, transform));
            meatPool.Add(Instantiate(meat, this.transform.position, Quaternion.identity, transform));
        }
    }
}