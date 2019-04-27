﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-250)]
public class TrapGenerator : GeneratorBase
{
    public float dis = 0;

    public bool check = false;

    private float CheckTrapDistance = 15;
    private float TrapSpeed = 5;

    private Vector3 TrapVelocityDir = Vector3.zero;

    private Vector3 ArrivalPosition = Vector3.zero;

    private void OnEnable()
    {
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        Process();
        state.Agent.velocity = TrapVelocityDir;

        ArrivalPosition = transform.position + transform.forward * 15;
    }

    public void Process()
    {
        unit.state = StateIndex.TRAP;
    }

    private void Advance()
    {
        unit.Anim.SetBool("RushAttack", true);
        transform.position = Vector3.Lerp(transform.position, ArrivalPosition, Time.deltaTime * TrapSpeed);
        dis = Vector3.Distance(transform.position, ArrivalPosition);
        if (dis <= 1.2f)
        {
            StartCoroutine(GetComponent<pomulseon>().MonsterStanUpStateChange(transform, StateIndex.Gallery, 0.9f));

            //state.ChangeState(StateIndex.Gallery);
        }
    }

    public override void Execution() //Update
    {
        if (unit.distance <= 15)
        {
            check = true;
        }
        if (check)
            Advance();
    }

    public override void Exit()
    {
    }
}