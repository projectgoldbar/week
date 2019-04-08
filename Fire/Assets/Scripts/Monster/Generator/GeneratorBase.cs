using UnityEngine;
using UnityEngine.AI;

public class GeneratorBase : MonoBehaviour
{
    protected Animator anim;
    protected NavMeshAgent agent;
    protected MonsterUnit unit;

    public virtual void Process()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<MonsterUnit>();
    }

    // Update is called once per frame
}