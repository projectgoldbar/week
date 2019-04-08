using UnityEngine;

public class State_Attack : State
{
    public State_Attack(MonoBehaviour mono) : base(mono)
    {
    }

    public override void Initiate()
    {
        var AttackProcessGenarator = Mono.gameObject.GetComponent<AttackGenerator>();
        if (AttackProcessGenarator == null)
            AttackProcessGenarator = Mono.gameObject.AddComponent<AttackGenerator>();
    }

    public override void Exit()
    {
        var AttackProcessGenarator = Mono.GetComponent<AttackGenerator>();
        if (AttackProcessGenarator != null)
        {
            Object.Destroy(AttackProcessGenarator);
        }
    }

    public override StateIndex NextState()
    {
        return StateIndex.CHASE;
    }
}