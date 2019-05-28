using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwaner : MonoBehaviour
{
    public GameObject spwanPointss;
    public Transform[] spwanPoints;
    public GameObject zombie;
    public GameObject weekZombie;
    public GameObject patrolZombie;
    public float second = 1f;
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
        float count = 0;
        while (count < 45)
        {
            SpwanZombie();
            count++;
            yield return seconds;
        }
        yield break;
    }

    public List<GameObject> patrolZombieList;

    private IEnumerator SpwanSystem()
    {
        for (int i = 0; i < 20; i++)
        {
            var monster = Instantiate(weekZombie, spwanPoints[Random.Range(1, spwanPoints.Length + 1)].position, Quaternion.identity);
            var patrolZombie = Instantiate(this.patrolZombie, spwanPoints[Random.Range(1, spwanPoints.Length + 1)].position, Quaternion.identity);
            patrolZombieList.Add(patrolZombie);
            yield return new WaitForSeconds(1f);
        }
    }
}