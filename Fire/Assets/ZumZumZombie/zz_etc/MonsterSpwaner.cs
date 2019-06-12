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

    private void Awake()
    {
        spwanPoints = spwanPointss.GetComponentsInChildren<Transform>();
        manager = GetComponent<Manager>();
        //StartCoroutine(Spwan());
        seconds = new WaitForSeconds(second);
        zombies.Add(zombie);
        zombies.Add(bombZombie);
        zombies.Add(patrolZombie);
    }

    public void SpwanZombie(GameObject zombie)
    {
        var monster = GameObject.Instantiate(zombie, spwanPoints[Random.Range(1, spwanPoints.Length)].position, Quaternion.identity, transform);
    }

    private IEnumerator Spwan()
    {
        int count = 0;
        while (count < 55)
        {
            SpwanZombie(zombie);
            count++;

            yield return seconds;
        }
        StartCoroutine(SpwanSystem());
        yield break;
    }

    public List<GameObject> patrolZombieList;

    private IEnumerator SpwanSystem()
    {
        WaitForSeconds tenSec = new WaitForSeconds(10f);
        for (int i = 0; i < 3; i++)
        {
            var monster = Instantiate(bombZombie, spwanPoints[Random.Range(1, spwanPoints.Length)].position, Quaternion.identity);
            var patrolZombie = Instantiate(this.bombZombie, spwanPoints[Random.Range(1, spwanPoints.Length)].position, Quaternion.identity);
            patrolZombieList.Add(patrolZombie);
            yield return new WaitForSeconds(20f);
        }

        for (int i = 0; i < patrolZombieList.Count; i++)
        {
            if (patrolZombieList[i].gameObject.activeSelf == false)
            {
                patrolZombieList[i].transform.position = spwanPoints[Random.Range(1, spwanPoints.Length)].position;
                patrolZombieList[i].gameObject.SetActive(true);
            }
            yield return tenSec;
        }
    }
}