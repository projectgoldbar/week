using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(500)]
public class TestRushZombie : MonoBehaviour
{
    public NavMeshAgent agent;
    public NavMeshPath Path;
    private Vector3 target;
    private bool coolDown = false;

    private void Awake()
    {
        Path = new NavMeshPath();
        target = Utility.Instance.playerTr.GetComponent<TestTarget>().target;
    }

    private void Start()
    {
        StartCoroutine(CalculatePath());
        StartCoroutine(CoolDown());
    }

    private void Update()
    {
        if (coolDown)
        {
            if (DIstanceChk() < 10f)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Debug.Log("a");
        Vector3.Lerp(transform.position, transform.forward + transform.forward * 3f, 0.3f);
    }

    //경로 탐색
    private IEnumerator CalculatePath()
    {
        while (true)
        {
            agent.CalculatePath(target, Path);
            agent.SetPath(Path);
            yield return new WaitForSeconds(0.3f);
        }
    }

    //공격하기까지의 텀
    private IEnumerator CoolDown()
    {
        while (true)
        {
            if (coolDown)
            {
                coolDown = true;
            }
            else
            {
                coolDown = false;
            }
            yield return new WaitForSeconds(3f);
        }
    }

    //거리계산
    private float DIstanceChk()
    {
        Vector3 dir = (target - transform.position);
        float distance = dir.magnitude;
        return distance;
    }
}