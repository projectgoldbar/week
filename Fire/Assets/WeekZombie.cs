using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeekZombie : MonoBehaviour
{
    public Transform target;

    public float thinkingTime = 1f;
    public Transform playerData;

    private WayPoint wayPoint;
    private WaitForSeconds waitForOneSeconds;
    private WaitForSeconds waitFor02Seconds;
    private NavMeshAgent agent;

    private void Awake()
    {
        wayPoint = FindObjectOfType<WayPoint>();
        playerData = FindObjectOfType<PlayerData>().transform;
        agent = GetComponent<NavMeshAgent>();
        waitForOneSeconds = new WaitForSeconds(1f);
        waitFor02Seconds = new WaitForSeconds(0.2f);
    }

    private void Start()
    {
        StartCoroutine(CheckingNextMove());
    }

    private IEnumerator CheckingNextMove()
    {
        while (true)
        {
            yield return null;
            if (Vector3.Distance(playerData.position, transform.position) > 30f)
            {
                SuchWayPoint();
                agent.SetDestination(target.position);
                yield return waitForOneSeconds;
            }
            else
            {
                SuchPlayer();
                agent.SetDestination(target.position);
                yield return waitFor02Seconds;
            }
        }
    }

    private void SuchWayPoint()
    {
        target = wayPoint.waypoints[Random.Range(0, wayPoint.waypoints.Length)];
    }

    private void SuchPlayer()
    {
        target = playerData;
    }
}