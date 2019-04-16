using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GeneratorBase : MonoBehaviour
{
    protected MonsterState state;
    protected MonsterUnit unit;
    //protected NavMeshAgent agent;

    public virtual void Awake()
    {
        state = transform.GetComponent<MonsterState>();
        unit = transform.GetComponent<MonsterUnit>();
        // agent = transform.GetComponent<NavMeshAgent>();
    }

    public virtual void Initiate()
    {
    }

    public virtual void Execution()
    {
    }

    public virtual void Exit()
    {
        StopAllCoroutines();
    }
}