using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwaner : MonoBehaviour
{
    public GameObject spwanPointss;
    public Transform[] spwanPoints;
    public GameObject zombie;
    public GameObject bombZombie;
    public GameObject patrolZombie;
    public float second = 1f;
    private WaitForSeconds seconds;
    private Manager manager;
    public List<GameObject> zombies;
    public int spwanCount = 4;

    private void Awake()
    {
        spwanPoints = spwanPointss.GetComponentsInChildren<Transform>();
        manager = GetComponent<Manager>();
        StartCoroutine(Spwan());
        seconds = new WaitForSeconds(second);
        zombies.Add(zombie);
        zombies.Add(bombZombie);
        zombies.Add(patrolZombie);
    }

    public void SpwanZombie(GameObject zombie)
    {
        GameObject.Instantiate(zombie, spwanPoints[Random.Range(1, spwanPoints.Length)].position, Quaternion.identity, transform);
    }

    private IEnumerator Spwan()
    {
        int count = 0;
        while (count < spwanCount)
        {
            SpwanZombie(zombie);
            count++;

            yield return seconds;
        }
        yield break;
    }

    
}