using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-200)]
public class NewMonsterGenerator : MonoBehaviour
{
    public Transform gp;
    public Transform lp;
    public Transform[] groundGenPoint;
    public Transform[] InteractionGenPoint;
    public MonsterDataBase monsterDataBase;
    public Transform zombie;
    public Transform Interaction;

    public float maxZombieCount = 300;

    public float cooldown = 0.3f;

    private WaitForSeconds second;

    private void Awake()

    {
        groundGenPoint = gp.GetComponentsInChildren<Transform>();
        InteractionGenPoint = lp.GetComponentsInChildren<Transform>();
        second = new WaitForSeconds(cooldown);
    }

    private void Start()
    {
        //GenerateInteraction();
    }

    public void GenGallery()
    {
        second = new WaitForSeconds(cooldown);
        StartCoroutine(Invokee(second));
    }

    private IEnumerator Invokee(WaitForSeconds cooldown)
    {
        for (int i = 0; i < maxZombieCount; i++)
        {
            GenerateZombie();
            yield return cooldown;
        }
    }

    private void GenerateZombie()
    {
        for (int i = 0; i < 1; i++)
        {
            float x = Random.Range(0, 6f);
            float z = Random.Range(0, 6f);
            var monster = GameObject.Instantiate(zombie, groundGenPoint[Random.Range(0, groundGenPoint.Length)].position + new Vector3(x, 0, z), Quaternion.identity);
            monsterDataBase.monsterList.Add(monster);
        }
    }

    private void GenerateInteraction()
    {
        for (int i = 0; i < InteractionGenPoint.Length; i++)
        {
            var gen = Random.Range(0, 2);
            if (gen == 0)
            {
                Instantiate(Interaction, InteractionGenPoint[i].position, Quaternion.identity);
            }
            else
            {
                continue;
            }
        }
    }
}