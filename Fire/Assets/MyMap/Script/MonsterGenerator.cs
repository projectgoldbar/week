using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public Transform player;

    public int MonsterCount;

    public Transform enemy;
    public float minDistance;
    public float maxDistance;

    private void Start()
    {
        StartCoroutine(Invoking());
    }

    private IEnumerator Invoking()
    {
        for (; ; )
        {
            MonsterGen(MonsterCount, minDistance, maxDistance);
            //yield break;
            yield return new WaitForSeconds(5f);
        }
    }

    public void MonsterGen(int NumOfMonster, float minDistance, float maxDistance)
    {
        for (int i = 0; i < NumOfMonster; i++)
        {
            GameObject.Instantiate(enemy, FindFarPoint(player.position, minDistance, maxDistance), Quaternion.identity);
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