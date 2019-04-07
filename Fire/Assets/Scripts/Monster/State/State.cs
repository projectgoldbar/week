using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    protected MonoBehaviour Mono;
    protected MonsterUnit monster;
    protected MonsterState state;
    protected NavMeshAgent agent;
    protected bool ChangeCheck = false;

    public State(MonoBehaviour mono)
    {
        this.Mono = mono;
        monster = Mono.GetComponent<MonsterUnit>();
        state = Mono.GetComponent<MonsterState>();
        agent = mono.GetComponent<NavMeshAgent>();
    }

    public virtual void Initiate()
    {
    }

    public virtual void Ing()
    {
    }

    public virtual void Exit()
    {
        Mono.StopAllCoroutines();
    }

    public virtual IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    public virtual StateIndex NextState()
    {
        return StateIndex.IDLE;
    }

    public virtual StateIndex NextState(StateIndex state)
    {
        return state;
    }
}