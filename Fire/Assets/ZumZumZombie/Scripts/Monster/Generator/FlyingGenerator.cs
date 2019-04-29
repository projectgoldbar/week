using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyingGenerator : GeneratorBase
{
    private pomulseon Fly;

    private Vector3 Arrival = Vector3.zero;

    private void OnEnable()
    {
    }

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
        transform.SetParent(null);

        if (state.Type == FlyType.Car)
        {
            Arrival = transform.position;
            Fly.FlyToTarget(transform,
                   transform.position,
                   Arrival,
                   (tr) => MonsterFly(transform));
        }
        else //if (state.Type == FlyType.Hulk)
        {
            //unit.Hulk = null;
            //Arrival = transform.position + transform.forward * 30.0f;
            Arrival = unit.Hulk.transform.position + unit.Hulk.transform.forward * 30.0f;
            Fly.FlyToTarget(transform,
               transform.position,
               Arrival,
               (tr) => MonsterFly(transform), 19.8f, 10.0f);
        }
    }

    public void MonsterFly(Transform target)
    {
        StartCoroutine(Fly.MonsterStanUpStateChange(target, StateIndex.CHASE));
    }

    public override void Exit()
    {
    }
}