using UnityEngine;

public class State_Sturn : State
{
    public State_Sturn(MonoBehaviour mono) : base(mono)
    {
    }

    public override void Initiate()
    {
    }

    public override void Exit()
    {
    }

    public override StateIndex NextState()
    {
        return StateIndex.CHASE;
    }
}