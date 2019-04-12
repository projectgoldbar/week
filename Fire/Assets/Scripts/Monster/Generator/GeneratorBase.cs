using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GeneratorBase : MonoBehaviour
{
    public MonsterState state;

    public MonsterUnit unit;

    public void Awake()
    {
        state = transform.GetComponent<MonsterState>();
        unit = transform.GetComponent<MonsterUnit>();
    }

    public virtual void Initiate()
    {
        Get(state, unit);
    }

    public virtual void Exit()
    {
    }

    public void Get(MonsterState state, MonsterUnit unit)
    {
        state = transform.GetComponent<MonsterState>();
        unit = transform.GetComponent<MonsterUnit>();
    }
}