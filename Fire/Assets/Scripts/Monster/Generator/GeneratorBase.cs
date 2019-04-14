using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GeneratorBase : MonoBehaviour
{
    protected MonsterState state;
    protected MonsterUnit unit;

    public virtual void Initiate()
    {
        state = transform.GetComponent<MonsterState>();
        unit = transform.GetComponent<MonsterUnit>();
    }

    public virtual void Execution()
    {
    }

    public virtual void Exit()
    {
        StopAllCoroutines();
    }
}