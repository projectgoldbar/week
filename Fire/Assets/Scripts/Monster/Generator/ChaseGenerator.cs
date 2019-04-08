using System;
using System.Collections;
using UnityEngine;

public enum ChaseState { Chase, Attack };

public class ChaseGenerator : GeneratorBase
{
    public ChaseState state = ChaseState.Chase;

    private Coroutine SetAgentPosition;

    public void OnEnable()
    {
        Process();
    }

    public override void Process()
    {
        base.Process();
        //agent.destination = Ref.Instance.playerTr.position;
        SetAgentPosition = StartCoroutine(AgentSetPosition());
    }

    public void ShaseProcess()
    {
        state = ChaseState.Chase;
        stateCheck(state);
    }

    public void AttackProcess()
    {
        if (unit.AttackCheck)
        {
            state = ChaseState.Attack;
            stateCheck(state);
        }
    }

    private void stateCheck(ChaseState State)
    {
        switch (State)
        {
            case ChaseState.Chase:
                anim.SetBool("Walk", true);
                agent.stoppingDistance = 0;
                //StartCoroutine()
                //agent.destination = Ref.Instance.playerTr.position;
                break;

            case ChaseState.Attack:
                anim.SetTrigger("Attack");
                agent.stoppingDistance = 0;
                StopCoroutine(SetAgentPosition);
                agent.destination = transform.position;
                break;
        }
    }

    public IEnumerator AgentSetPosition()
    {
        for (; ; )
        {
            agent.destination = Ref.Instance.playerTr.position;
            yield return new WaitForSeconds(0.02f);
        }
    }
}