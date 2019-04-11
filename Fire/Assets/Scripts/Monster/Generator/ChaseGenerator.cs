using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseGenerator : GeneratorBase
{
    private NavMeshPath Path;

    private void Awake()
    {
        Path = new NavMeshPath();
    }

    public override void Initiate()
    {
        base.Initiate();
        Process();
    }

    public override void Exit()
    {
        generator.enabled = false;
    }

    private void Process()
    {
        generator.enabled = true;
    }

    public void ShaseProcess()
    {
        var FindTarget = Physics.OverlapSphere(unit.Righthand.transform.position, 2.0f, 1 << LayerMask.GetMask("Player"));
        if (FindTarget.Length > 0)
            Debug.Log(FindTarget[0]);

        unit.state = StateIndex.CHASE;
        stateCheck(unit.state);
    }

    public void AttackProcess()
    {
        if (unit.AttackCheck)
        {
            unit.state = StateIndex.ATTACK;
            stateCheck(unit.state);
        }
    }

    private void stateCheck(StateIndex State)
    {
        switch (State)
        {
            case StateIndex.CHASE:
                unit.Anim.SetBool("Walk", true);
                monsterState.Agent.avoidancePriority = 50;
                break;

            case StateIndex.ATTACK:
                unit.Anim.SetTrigger("Attack");
                monsterState.Agent.avoidancePriority = 51;
                break;
        }
    }

    public void Update()
    {
        NavMesh.CalculatePath(transform.position, Ref.Instance.playerTr.position, NavMesh.AllAreas, Path);

        monsterState.Agent.SetPath(Path);
    }

    //public IEnumerator AgentSetPosition()
    //{
    //    NavMeshPath Path = new NavMeshPath();

    //    for (; ; )
    //    {
    //        NavMesh.CalculatePath(transform.position, Ref.Instance.playerTr.position, NavMesh.AllAreas, Path);

    //        monsterState.Agent.SetPath(Path);
    //        yield return Second;
    //    }
    //}
}