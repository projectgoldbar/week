using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGenerator : GeneratorBase
{
    private pomulseon Fly;

    public override void Awake()
    {
        base.Awake();
        Fly = GetComponent<pomulseon>();
    }

    public override void Initiate()
    {
        state.Agent.enabled = false;
        unit.state = StateIndex.Flaying;
        unit.Anim.SetBool("RushAttack", true);

        Fly.FlyToTarget(transform,
               transform.position,
               transform.position);
    }

    public override void Exit()
    {
    }
}