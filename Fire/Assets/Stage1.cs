using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public override void Setting()
    {
        var bounds = GetComponent<BoxCollider>().bounds;
    }
}