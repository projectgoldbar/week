using UnityEngine;

public class State_Jump : State
{
    public State_Jump(MonoBehaviour mono) : base(mono)
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
        return StateIndex.STURN;
    }
}