using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOutCatch : GeneratorBase
{
    //헐크한테 잡힌 상태

    public void OnEnable()
    {
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        base.Initiate();
        Debug.Log("잡힌상태로 왔다");

        unit.state = StateIndex.FlyOutCatch;
        Process(unit.Hulk);

        //잡혓슴.
    }

    private void Process(Transform Catch)
    {
        state.Agent.enabled = false;
        state.State_Inactive(this);

        Catch.GetComponent<ThrowingSearch>().SearchIn = true;
        transform.SetParent(Catch.transform);
        Catch.GetComponent<MonsterUnit>().GetCatch(transform);
    }

    public override void Execution()
    {
        if (unit.Hulk)
        {
            transform.position = unit.Hulk.transform.position + Vector3.up * 3.0f;
            //transform.rotation = Quaternion.Euler(-90.0f, 0, 90.0f);

            if (this.enabled == false)
            {
                state.ChangeState(StateIndex.FlyOutCatch);
            }
        }
    }
}