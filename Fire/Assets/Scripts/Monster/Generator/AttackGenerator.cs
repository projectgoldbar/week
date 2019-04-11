using System.Collections;
using UnityEngine;

public class AttackGenerator : GeneratorBase
{
    private void OnEnable()
    {
        Process();
    }

    public void Process()
    {
        // anim.SetBool("Attack", true);
        monsterState.Agent.stoppingDistance = 0;
        // monsterState.Agent.destination = transform.position;
    }
}