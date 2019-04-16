using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AttackGenerator : GeneratorBase
{
    public AttackKind attackKind = AttackKind.RUSH_ATTACK;

    private float distance = 40.0f;

    private Material mat;
    private bool attackStanby = false;
    private Vector3 pos = new Vector3();

    public override void Awake()
    {
        base.Awake();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
    }

    private void OnEnable()
    {
        agent.speed = 0;
        agent.ResetPath();
        attackStanby = false;
    }

    public override void Initiate()
    {
        Process();
        StartCoroutine(RushStanby());

        pos = transform.position + transform.forward * distance;
    }

    private void Process()
    {
        state.Agent.ResetPath();

        attackKind = AttackKind.RUSH_ATTACK;
        unit.state = StateIndex.ATTACK;
    }

    public IEnumerator RushStanby()
    {
        for (int i = 0; i < 2; i++)
        {
            mat.color = Utility.Instance.ChangeColor(Color.green);

            yield return new WaitForSeconds(0.05f);

            mat.color = Utility.Instance.ChangeColor(Color.white);

            yield return new WaitForSeconds(0.05f);
        }

        ATKKindProcess();
    }

    public void ATKKindProcess()
    {
        if (attackKind == AttackKind.RUSH_ATTACK)
        {
            attackStanby = true;
            Invoke("RushAttackProcess", 0.3f);
        }
    }

    public void RushAttackProcess()
    {
        //러쉬공격
        unit.Anim.SetBool("RushAttack", true);
        //unit.Attack = true;
        state.Agent.updateRotation = false;

        Forward_Direction_Control();

        Invoke("StateEnd", 0.5f);
    }

    public void Forward_Direction_Control()
    {
        Quaternion rot = Quaternion.LookRotation(Utility.Instance.playerTr.position - transform.position);
        transform.rotation = rot;
    }

    private void StateEnd()
    {
        // unit.Attack = false;
        unit.Anim.SetBool("RushAttack", false);
        state.Agent.updateRotation = true;
        state.ChangeState(StateIndex.CHASE);
    }

    public override void Execution()
    {
        //if (unit)
        if (!attackStanby) return;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 3.0f);

        //if (Vector3.Distance(transform.position, pos) < 5f)
        //{
        //    StateEnd();
        //}
    }

    public override void Exit()
    {
    }

    private void OnDisable()
    {
        agent.speed = 10f;
    }

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 6f, float maxDistance = 10f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(unit.ChaseTarget.rotation.eulerAngles.y * -1, -unit.ChaseTarget.rotation.eulerAngles.y + 180);
        float radian = angle * Mathf.Deg2Rad;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }
}