using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PatrolGenerator : GeneratorBase
{
    public Transform patrolPointParent;
    public Transform[] Points;
    private Coroutine PatrolRutine;

    private Action StopRutine;

    private void OnEnable()
    {
    }

    public override void Initiate()
    {
        base.Initiate();
        Process();
    }

    private void Process()
    {
        Points = PatrolPoint();
        Randomdestination();

        PatrolRutine = StartCoroutine(PathPending());
        StopRutine += Stop;
        //Process1();
    }

    private void Stop()
    {
        StopCoroutine(PatrolRutine);
    }

    public override void Exit()
    {
        StopRutine?.Invoke();
    }

    private Transform[] PatrolPoint()
    {
        Points = patrolPointParent.GetComponentsInChildren<Transform>();

        return Points;
    }

    private void Randomdestination()
    {
        monsterState.Agent.SetDestination(Points[UnityEngine.Random.Range(0, Points.Length)].transform.position);
    }

    private IEnumerator PathPending()
    {
        while (true)
        {
            if (!monsterState.Agent.pathPending && monsterState.Agent.remainingDistance <= 2.0f)
            {
                Randomdestination();
            }
            yield return new WaitForSeconds(0.02f);
            yield return null;
        }
    }

    #region 왔다갓다

    private void Process1()
    {
        RandomAngle random = new RandomAngle();

        unit.Anim.SetBool("Walk", true);
        monsterState.Agent.stoppingDistance = 0;
        var randomPos = transform.position + random.RandomPosition();
        monsterState.Agent.destination = randomPos;

        Invoke("Process2", UnityEngine.Random.Range(2, 5));
    }

    private void Process2()
    {
        unit.Anim.SetBool("Walk", false);
        monsterState.Agent.stoppingDistance = 0;
        monsterState.Agent.destination = transform.position;
        Invoke("Process1", UnityEngine.Random.Range(2, 5));
    }

    #endregion 왔다갓다
}