using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chase : State
{
    public override StateIndex NextState()
    {
        return StateIndex.ATTACK;
    }
}