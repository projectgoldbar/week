using UnityEngine;

public class State_Patrol : State
{
    public State_Patrol(MonoBehaviour mono) : base(mono)
    { }

    public override void Initiate()
    {
        Debug.Log("Patrol");
        var PatrolProcessGenarator = Mono.gameObject.GetComponent<PatrolGenerator>();
        if (PatrolProcessGenarator == null)
            PatrolProcessGenarator = Mono.gameObject.AddComponent<PatrolGenerator>();
    }

    public override void Ing()
    {
        if (ChangeCheck && monster.Check) return;
        if (monster.Check)
        {
            ChangeCheck = true;
            state.ChangeState(StateIndex.CHASE);
        }
    }

    public override void Exit()
    {
        Debug.Log("Patrol-Exit");
        var PatrolProcessGenarator = Mono.GetComponent<PatrolGenerator>();
        if (PatrolProcessGenarator != null)
        {
            Object.Destroy(PatrolProcessGenarator);
        }
    }

    public override StateIndex NextState()
    {
        return StateIndex.IDLE;
    }
}