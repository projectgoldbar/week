using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[DefaultExecutionOrder(-600)]
public class Test : MonoBehaviour
{
    private NavMeshPath path;
    private NavMeshAgent agent;
    private WaitForSeconds second;

    private Transform tr;

    private void Awake()
    {
        path = new NavMeshPath();
        second = new WaitForSeconds(0.3f);
        tr = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
    }

    private void OnEnable()
    {
        path = new NavMeshPath();
        second = new WaitForSeconds(0.3f);
        tr = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
        StartCoroutine(CalculatePath());
    }

    public IEnumerator CalculatePath()
    {
        while (true)
        {
            agent.ResetPath();
            agent.CalculatePath(tr.position, path);
            agent.SetPath(path);
            yield return second;
        }
    }
}