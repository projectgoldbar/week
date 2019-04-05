using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Jump : State
{
    public override StateIndex NextState()
    {
        return StateIndex.STURN;
    }
}