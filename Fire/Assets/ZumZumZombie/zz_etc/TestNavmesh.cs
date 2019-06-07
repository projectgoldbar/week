using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavmesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public NavMeshPath path;
    public Transform target;
    public float distance;

    private void Start()
    {
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
        StartCoroutine(CheckingNextMove());
    }

    private IEnumerator CheckingNextMove()
    {
        while (true)
        {
            DistanceCheck();
            if (distance < 50f)
            {
                StartCoroutine(Stoking());
            }
            else if (distance < 100f)
            {
                Walking();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void DistanceCheck()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }

    private IEnumerator Stoking()
    {
        //for (float i = distance; i < 50;)
        //{
        bool chk = true;
        while (chk)
        {
            var direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            var a = Quaternion.LookRotation(direction);
            transform.rotation = a;
            agent.velocity = agent.transform.forward * agent.speed;
            if (distance < 50)
            {
                chk = false;
            }
            yield return null;
        }
        yield break;
    }

    private void Walking()
    {
        agent.ResetPath();
        agent.CalculatePath(target.position, path);
        agent.SetPath(path);
    }
}