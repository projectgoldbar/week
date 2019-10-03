using UnityEngine;
using UnityEngine.AI;

public class GeneratorBase : MonoBehaviour
{
    protected MonsterState state;
    protected MonsterUnit unit;
    protected NavMeshPath Path;
    //protected NavMeshAgent agent;

    public virtual void Awake()
    {
        state = transform.GetComponent<MonsterState>();
        unit = transform.GetComponent<MonsterUnit>();
        Path = new NavMeshPath();
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