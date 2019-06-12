using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies_Position : MonoBehaviour
{
    public Vector3[] DefaultDistance;
    
    public Transform[] ZombiesWayPosition;

    private float Zpos = 0;
    private float Xpos = 0;

    public int ZombieMaxCount = 10;


    public void Defaultpos()
    {

        for (int i = 0; i < ZombieMaxCount; i++)
        {
            Zpos = Random.Range(0f, 12f);
            Xpos = Random.Range(0f, 12f);

            var  pos = new Vector3(Xpos, 0, Zpos);

            DefaultDistance[i] = pos;
        }

        var bb = GetVecto3Array();
    }

    public Vector3[] GetVecto3Array()
    {
        for (int i = 0; i < ZombieMaxCount; i++)
        {
            DefaultDistance[i] += transform.position;

            var obj = new GameObject(i.ToString());
            obj.transform.SetParent(transform);
            obj.transform.position = DefaultDistance[i];
            ZombiesWayPosition[i] = obj.transform;
        }

        return DefaultDistance;
    }

    private void Update()
    {
        for (int i = 0; i < ZombieMaxCount; i++)
        {
            DefaultDistance[i] = ZombiesWayPosition[i].position;
        }
    }
}
