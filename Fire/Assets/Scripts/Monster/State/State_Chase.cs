using UnityEngine;

public class State_Chase : State
{
    public State_Chase(MonoBehaviour mono) : base(mono)
    {
    }

    public override void Initiate()
    {
        Debug.Log("Chase");
        var ChaseProcessGenarator = Mono.gameObject.GetComponent<ChaseGenerator>();
        if (ChaseProcessGenarator == null)
            ChaseProcessGenarator = Mono.gameObject.AddComponent<ChaseGenerator>();
    }

    public override void Exit()
    {
        Debug.Log("Chase-Exit");
        var ChaseProcessGenarator = Mono.GetComponent<ChaseGenerator>();
        if (ChaseProcessGenarator == null) return;
        Object.Destroy(ChaseProcessGenarator);
    }

    public override StateIndex NextState()
    {
        return StateIndex.ATTACK;
    }
}