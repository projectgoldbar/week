using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-500)]
public class ThrowingChase : ChaseGenerator
{
    private void OnEnable()
    {
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        base.Initiate();
    }

    public override void Process()
    {
        unit.state = StateIndex.CHASE;
        state.Agent.enabled = true;
    }

    public override void CoolDown()
    {
        if (unit.distance <= 10.0f && unit.Catch)
        {
            state.ChangeState(StateIndex.ATTACK);
            //던지는상태로 변경
        }
    }
}