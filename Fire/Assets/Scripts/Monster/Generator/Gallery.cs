using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-500)]
public class Gallery : ChaseGenerator
{
    public override void Initiate()
    {
        base.Initiate();
        second = new WaitForSeconds(0.2f);
    }

    public override void CoolDown()
    {
    }
}