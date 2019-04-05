using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State
{
    public override StateIndex NextState()
    {
        return StateIndex.CHASE;
    }
}