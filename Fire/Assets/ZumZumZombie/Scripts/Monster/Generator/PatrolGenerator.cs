using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PatrolGenerator : GeneratorBase
{
    public Transform patrolPointParent = null;
    public Transform[] Points;

    private WaitForSeconds second = new WaitForSeconds(0.05f);
    private NavMeshPath Path;

    private void OnEnable()
    {
        Path = new NavMeshPath();
    }

    public override void Initiate()
    {
        base.Initiate();
        Process();
    }

    private void Process()
    {
        unit.state = StateIndex.PATROL;
        Points = PatrolPoint();
        Randomdestination();

        //Process1();
    }

    public override void Exit()
    {
    }

    private Transform[] PatrolPoint()
    {
        Points = patrolPointParent.GetComponentsInChildren<Transform>();

        return Points;
    }

    private void Randomdestination()
    {
        state.Agent.destination = Points[Random.Range(0, Points.Length)].transform.position;
    }

    public override void Execution()
    {
        if (!state.Agent.pathPending && state.Agent.remainingDistance <= 2.0f)
        {
            Randomdestination();
        }
    }

    private void Process1()
    {
        RandomAngle random = new RandomAngle();

        unit.Anim.SetBool("Walk", true);
        state.Agent.stoppingDistance = 0;
        var randomPos = transform.position + random.RandomPosition();
        state.Agent.destination = randomPos;

        Invoke("Process2", Random.Range(2, 5));
    }

    private void Process2()
    {
        unit.Anim.SetBool("Walk", false);
        state.Agent.stoppingDistance = 0;
        state.Agent.destination = transform.position;
        Invoke("Process1", Random.Range(2, 5));
    }
}