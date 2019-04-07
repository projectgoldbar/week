using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public NavMeshAgent Agent;
    public MonoBehaviour mono;
    public State StateBase = null;

    private StateIndex stateindex = StateIndex.IDLE;

    public void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        mono = GetComponent<MonoBehaviour>();
        ChangeState(StateIndex.PATROL);
    }

    private void Update()
    {
        if (StateBase != null)
        {
            StateBase.Ing();
        }
    }

    public void ChangeState(StateIndex nextState)
    {
        if (StateBase != null)
            StateBase.Exit();

        StateBase = CreateStateInstance(nextState);

        StateBase.Initiate();
    }

    public State CreateStateInstance(StateIndex NextState)
    {
        switch (NextState)
        {
            //case StateIndex.IDLE: return new State_Idle(mono);
            case StateIndex.PATROL: StateBase = new State_Patrol(mono); break;
            case StateIndex.CHASE: StateBase = new State_Chase(mono); break;
            case StateIndex.ATTACK: StateBase = new State_Attack(mono); break;
            case StateIndex.JUMP: StateBase = new State_Jump(mono); break;
            case StateIndex.STURN: StateBase = new State_Sturn(mono); break;
        }
        return StateBase;
    }
}