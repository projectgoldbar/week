using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingChase : ChaseGenerator
{
    private float distance = 0;

    public override void Awake()
    {
        base.Awake();
    }

    public override void CoolDown()
    {
        distance = unit.Distance;

        if (distance <= 10.0f)
        {
            //던지는상태로 변경
            //state.ChangeState(StateIndex.ATTACK);
        }
    }
}