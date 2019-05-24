using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwaner : MonoBehaviour
{
    public GameObject spwanPointss;
    public Transform[] spwanPoints;
    public GameObject zombie;
    public float second = 10f;
    private WaitForSeconds seconds;

    private void Awake()
    {
        spwanPoints = spwanPointss.GetComponentsInChildren<Transform>();
        StartCoroutine(Spwan());
        seconds = new WaitForSeconds(second);
    }

    public void SpwanZombie()
    {
        var monster = GameObject.Instantiate(zombie, spwanPoints[Random.Range(1, spwanPoints.Length)].position, Quaternion.identity);
    }

    private IEnumerator Spwan()
    {
        while (true)
        {
            SpwanZombie();

            yield return seconds;
        }
    }
}