using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Patrol : State
{
    public override StateIndex NextState()
    {
        if (true)
        {
            return StateIndex.CHASE;
        }
        else
        {
            return StateIndex.JUMP;
        }
    }
}