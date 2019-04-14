using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public Transform player;
    public MonsterDataBase monsterDataBase;

    public int MonsterCount;

    public Transform enemy;

    public float duration = 1;
    public int time = 4;
    public float minDistance;
    public float maxDistance;

    private Queue<Transform> monsterQueue;

    private void Awake()
    {
        Screen.SetResolution(720, 1280, true);
    }

    private void Start()
    {
        StartCoroutine(InvokeCreating(duration, time));
    }

    private IEnumerator InvokeCreating(float duration, int time)
    {
        for (int i = 0; i < time; i++)
        {
            MonsterGen(MonsterCount, minDistance, maxDistance);

            yield return new WaitForSeconds(duration);
        }
        yield break;
    }

    public void MonsterGen(int NumOfMonster, float minDistance, float maxDistance)
    {
        var a = monsterDataBase.monsterList;
        for (int i = 0; i < NumOfMonster; i++)
        {
            var monster = GameObject.Instantiate(enemy, FindFarPoint(player.position, minDistance, maxDistance), Quaternion.identity);
            a.Add(monster);
            monsterDataBase.monsterDataList.Add(new MonsterData(monster.transform));
        }
    }

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 6f, float maxDistance = 10f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(player.rotation.eulerAngles.y * -1, -player.rotation.eulerAngles.y + 180);
        float radian = angle * Mathf.Deg2Rad;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }
}