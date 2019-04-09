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
        StartCoroutine(AgentSetPosition());
    }

    public void ShaseProcess()
    {
        var FindTarget = Physics.OverlapSphere(unit.Righthand.transform.position, 2.0f, 1 << LayerMask.GetMask("Player"));
        if (FindTarget.Length > 0)
            Debug.Log(FindTarget[0]);

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

                break;

            case ChaseState.Attack:
                anim.SetTrigger("Attack");
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