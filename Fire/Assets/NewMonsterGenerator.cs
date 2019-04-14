using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonsterGenerator : MonoBehaviour
{
    public Transform gp;
    public Transform lp;
    public Transform[] groundGenPoint;
    public Transform[] loopTopGenPoint;
    public MonsterDataBase monsterDataBase;
    public Transform zombie;
    public Transform looptopZombie;

    public float maxZombieCount = 300;

    private void Awake()
    {
        groundGenPoint = gp.GetComponentsInChildren<Transform>();
        loopTopGenPoint = lp.GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        InvokeRepeating("GenerateZombie", 1f, 0.3f);
        GenerateLoopTopZombie();
    }

    private void GenerateZombie()
    {
        for (int i = 0; i < 1; i++)
        {
            float x = Random.Range(0, 6f);
            float z = Random.Range(0, 6f);
            var monster = GameObject.Instantiate(zombie, groundGenPoint[Random.Range(0, groundGenPoint.Length)].position + new Vector3(x, 0, z), Quaternion.identity);
            monsterDataBase.monsterList.Add(monster);
            monsterDataBase.monsterDataList.Add(new MonsterData(monster.transform));
        }
    }

    private void GenerateLoopTopZombie()
    {
        for (int i = 0; i < loopTopGenPoint.Length; i++)
        {
            var gen = Random.Range(0, 2);
            if (gen == 0)
            {
                Instantiate(looptopZombie, loopTopGenPoint[i].position, Quaternion.identity);
            }
            else
            {
                continue;
            }
        }
    }
}