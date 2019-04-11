using System.Collections;
using UnityEngine;

public class AttackGenerator : GeneratorBase
{
    private void OnEnable()
    {
        Process();
    }

    public override void Process()
    {
        base.Process();

        anim.SetBool("Attack", true);
        agent.stoppingDistance = 0;
        agent.destination = transform.position;
    }
}