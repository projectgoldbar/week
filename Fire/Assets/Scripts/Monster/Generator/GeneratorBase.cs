using UnityEngine;
using UnityEngine.AI;

public class GeneratorBase : MonoBehaviour
{
    protected MonsterState monsterState;
    protected MonsterUnit unit;

    protected GeneratorBase generator;

    public virtual void Initiate()
    {
        generator = this;
        monsterState = GetComponent<MonsterState>();
        unit = GetComponent<MonsterUnit>();
    }

    public virtual void Exit()
    {
    }
}