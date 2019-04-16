using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-500)]
public class TrapGenerator : GeneratorBase
{
    private Vector3 pos = new Vector3();

    public float dis = 0;

    public bool check = false;

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

        state.Agent.velocity = Vector3.zero;

        pos = transform.position + transform.forward * 15;
    }

    public void Process()
    {
        unit.state = StateIndex.TRAP;
    }

    private void Advance()
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 2.0f);
        dis = Vector3.Distance(transform.position, pos);
        if (dis <= 1.5f)
        {
            state.ChangeState(StateIndex.Gallery);
        }
    }

    public override void Execution() //Update
    {
        if (unit.Distance <= 15)
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