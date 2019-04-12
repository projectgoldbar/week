using System.Collections;
using UnityEngine;

public class AttackGenerator : GeneratorBase
{
    public AttackKind attackKind = AttackKind.HAND_ATTACK;

    private void OnEnable()
    {
    }

    public void HandAttackProcess()
    {
        unit.Anim.SetTrigger("Attack");
    }

    public void RushAttackProcess()
    {
        //러쉬공격
    }

    public override void Initiate()
    {
        base.Initiate();
        Process();
        if (attackKind == AttackKind.HAND_ATTACK)
        {
            HandAttackProcess();
        }
        else if (attackKind == AttackKind.RUSH_ATTACK)
        {
            RushAttackProcess();
        }
    }

    private void Process()
    {
        unit.state = StateIndex.ATTACK;
    }

    public override void Exit()
    {
    }
}