using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform target;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestCode();
        }
    }

    private void TestCode()
    {
        NavMeshPath navpath = new NavMeshPath();

        NavMesh.CalculatePath(this.transform.position, target.position, NavMesh.AllAreas, navpath);

        //for (int i = 0; i < navpath.corners.Length; i++)
        //{
        //    Debug.Log(navpath.corners[i]);
        //}
        navMeshAgent.SetPath(navpath);
    }
}