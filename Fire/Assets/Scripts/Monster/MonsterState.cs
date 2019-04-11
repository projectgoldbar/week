using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public NavMeshAgent Agent;
    public MonoBehaviour mono;
    // public State StateBase = null;

    [Header("안넣어도됨 상태머신에서 집어넣음.")]
    public GeneratorBase StateBase = null;

    private StateIndex stateindex = StateIndex.IDLE;

    private PatrolGenerator Patrol;
    private ChaseGenerator Chase;

    public void Awake()
    {
        Patrol = GetComponent<PatrolGenerator>();
        Patrol.enabled = false;
        Chase = GetComponent<ChaseGenerator>();
        Chase.enabled = false;
        ChangeState(StateIndex.PATROL);
    }

    public void ChangeState(StateIndex nextState)
    {
        if (StateBase != null)
            StateBase.Exit();

        StateBase = CreateStateInstance(nextState);

        StateBase.Initiate();
    }

    public GeneratorBase CreateStateInstance(StateIndex NextState)
    {
        GeneratorBase Base = null;

        switch (NextState)
        {
            //case StateIndex.IDLE: return new State_Idle(mono);
            case StateIndex.PATROL:
                Base = Patrol;
                Base.enabled = true;
                Chase.enabled = false;
                break;

            case StateIndex.CHASE:
                Base = Chase;
                Base.enabled = true;
                Patrol.enabled = false;
                break;
        }
        return Base;
    }
}