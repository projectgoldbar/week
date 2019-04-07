using UnityEngine;
using UnityEngine.AI;

public class GeneratorBase : MonoBehaviour
{
    protected Animator anim;
    protected NavMeshAgent agent;

    public virtual void Process()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
}