using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterStateTrap : MonsterState
{
    private void OnEnable()
    {
    }

    public override void Awake()
    {
        ListAddSetting();
        ChangeState(StateIndex.TRAP);
    }
}