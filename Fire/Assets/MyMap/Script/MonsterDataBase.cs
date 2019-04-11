using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataBase : MonoBehaviour
{
    public List<Transform> monsterList;
    public List<MonsterData> monsterDataList;

    private void Awake()
    {
        monsterDataList = new List<MonsterData>();
    }
}

public class MonsterData
{
    public Transform transform;
    public float distanceToPlayer;

    public MonsterData(Transform transform)
    {
        this.transform = transform;
        distanceToPlayer = 0;
    }
}