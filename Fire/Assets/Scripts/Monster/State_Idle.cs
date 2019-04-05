using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
{
    public override void Initiate()
    {
    }

    public override void Ing()
    {
    }

    public override StateIndex NextState()
    {
        if (true)
        {
            return StateIndex.PATROL;
        }
    }
}