using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOutCatch : GeneratorBase
{
    //헐크한테 잡힌 상태
    public Transform Hulk;

    public FlyOutCatch(Transform Child)
    {
        Hulk = Child;
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        base.Initiate();
        //잡혓슴.
    }

    public override void Exit()
    {
        base.Exit();
    }
}