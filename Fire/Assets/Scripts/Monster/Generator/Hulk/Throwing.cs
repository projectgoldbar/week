using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Throwing : Attack
{
    //던지는 상태
    public MonsterState Child;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        unit.state = StateIndex.ATTACK;
        GetComponent<ThrowingSearch>().SearchIn = false;

        Child = unit.Catch.GetComponent<MonsterState>();

        unit.Catch.GetComponent<MonsterState>().Type = FlyType.Hulk;

        unit.Catch.GetComponent<MonsterState>().ChangeState(StateIndex.Flaying);
        unit.Catch.transform.SetParent(null);

        state.ChangeState(StateIndex.CHASE);
    }

    public override void AttackProcess()
    {
    }

    public override void Execution()
    {
        state.Agent.CalculatePath(unit.ChaseTarget.position, Path);
        state.Agent.SetPath(Path);
    }

    public override void Exit()
    {
        unit.Catch = null;
    }
}