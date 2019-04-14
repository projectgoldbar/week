using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AttackGenerator : GeneratorBase
{
    public AttackKind attackKind = AttackKind.RUSH_ATTACK;

    private float distance = 10.0f;

    private Material mat;

    private void Awake()
    {
        mat = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
    }

    private void OnEnable()
    {
    }

    public override void Initiate()
    {
        base.Initiate();
        Process();

        ATKKind();
    }

    private void Process()
    {
        state.Agent.ResetPath();

        attackKind = AttackKind.RUSH_ATTACK;
        unit.state = StateIndex.ATTACK;

        // mat.color = Ref.Instance.RedColor();
    }

    public void ATKKind()
    {
        if (attackKind == AttackKind.RUSH_ATTACK)
        { Invoke("RushAttackProcess", 1.0f); }
    }

    public void RushAttackProcess()
    {
        //러쉬공격
        unit.Anim.SetBool("RushAttack", true);
        unit.Attack = true;
        state.Agent.updateRotation = false;

        Forward_Direction_Control();

        Invoke("StateEnd", 0.5f);
    }

    public void Forward_Direction_Control()
    {
        Quaternion rot = Quaternion.LookRotation(Ref.Instance.playerTr.position - transform.position);
        transform.rotation = rot;
    }

    private void StateEnd()
    {
        unit.Attack = false;
        unit.Anim.SetBool("RushAttack", false);
        state.ChangeState(StateIndex.CHASE);
    }

    public override void Execution()
    {
        if (!unit.Attack) return;

        transform.position = Vector3.Lerp(transform.position,
                                          transform.position + transform.forward * distance,
                                          Time.deltaTime * 5.0f);
    }

    public override void Exit()
    {
        // mat.color = Ref.Instance.NonColor();
        state.Agent.updateRotation = true;
    }

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 6f, float maxDistance = 10f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(unit.ChaseTarget.rotation.eulerAngles.y * -1, -unit.ChaseTarget.rotation.eulerAngles.y + 180);
        float radian = angle * Mathf.Deg2Rad;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }
}