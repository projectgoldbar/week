using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AttackGenerator : GeneratorBase
{
    public AttackKind attackKind = AttackKind.RUSH_ATTACK;

    public TrailRenderer trail = null;

    private float distance = 30.0f;

    private Material mat;
    private bool attackStanby = false;
    private Vector3 pos = new Vector3();

    private Vector3 hitpos = Vector3.zero;
    private RaycastHit hit;

    public override void Awake()
    {
        base.Awake();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
    }

    private void OnEnable()
    {
        state.Agent.ResetPath();
        attackStanby = false;
        state.Agent.velocity = Vector3.zero;
    }

    public override void Initiate()
    {
        Process();
        StartCoroutine(RushStanby());

        trail.time = 0.2f;
    }

    private bool isHit = false;

    private void RayCastFindPosition()
    {
        //isHit = (Physics.Raycast(transform.position, transform.forward, out hit, distance));

        //if (isHit)
        //{
        //    //hitpos = Vector3.Lerp(transform.position, hit.point, 0.7f);
        //    pos = hit.point - Vector3.forward * 1.0f;
        //}
        //else
        {
            pos = transform.position + transform.forward * distance;
        }

        // pos = transform.position + transform.forward * distance;
    }

    private void Process()
    {
        state.Agent.ResetPath();

        attackKind = AttackKind.RUSH_ATTACK;
        unit.state = StateIndex.ATTACK;

        RayCastFindPosition();
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
        state.Agent.updateRotation = false;
        unit.Anim.SetBool("RushAttack", true);

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
        state.ChangeState(StateIndex.CHASE);
    }

    public override void Execution()
    {
        if (!attackStanby) return;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 3.0f);
    }

    public override void Exit()
    {
        trail.time = 0.01f;
    }

    private void OnDisable()
    {
    }

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 6f, float maxDistance = 10f)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(unit.ChaseTarget.rotation.eulerAngles.y * -1, -unit.ChaseTarget.rotation.eulerAngles.y + 180);
        float radian = angle * Mathf.Deg2Rad;
        return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
    }
}