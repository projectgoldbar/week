using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Sturn : State
{
    public override StateIndex NextState()
    {
        return StateIndex.CHASE;
    }
}