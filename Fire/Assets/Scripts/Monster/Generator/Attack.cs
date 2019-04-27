using System.Collections;
using UnityEngine;

public class Attack : GeneratorBase
{
    public AttackKind attackKind = AttackKind.RUSH_ATTACK;

    public TrailRenderer trail;

    private float distance = 30f;

    private Material mat;
    private bool attackStanby = false;
    private Vector3 pos = new Vector3();

    private Vector3 hitpos = Vector3.zero;

    private pomulseon pomulseon;

    private bool isHit = false;

    public override void Awake()
    {
        base.Awake();
        pomulseon = GetComponent<pomulseon>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
    }

    private void OnEnable()
    {
        //state.Agent.ResetPath();
        attackStanby = false;
    }

    public override void Initiate()
    {
        Process();
        state.Agent.velocity = Vector3.zero;
        state.Agent.ResetPath();
        StartCoroutine(RushStanby());

        trail.time = 0.2f;
    }

    private void Process()
    {
        attackKind = AttackKind.RUSH_ATTACK;
        unit.state = StateIndex.ATTACK;

        RayCastFindPosition();
    }

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
        yield break;
    }

    public void ATKKindProcess()
    {
        if (attackKind == AttackKind.RUSH_ATTACK)
        {
            //state.Agent.updateRotation = false;
            attackStanby = true;
            AttackProcess();
        }
    }

    public virtual void AttackProcess()
    {
        //러쉬공격
        unit.Anim.SetBool("RushAttack", true);

        //Forward_Direction_Control();
    }

    public void Forward_Direction_Control()
    {
        Quaternion rot = Quaternion.LookRotation(Utility.Instance.playerTr.position - transform.position);
        transform.rotation = rot;
    }

    private void StateEnd()
    {
        StartCoroutine(pomulseon.MonsterStanUpStateChange(transform, StateIndex.CHASE));
    }

    private float CoolTimer = 1;
    private float CurrentTIme = 0;

    public override void Execution()
    {
        AttackEndCoolTime();
    }

    public void DashMv()
    {
        if (!attackStanby) return;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 3.0f);
    }

    public void AttackEndCoolTime()
    {
        DashMv();

        if (attackKind == AttackKind.RUSH_ATTACK)
        {
            CurrentTIme += Time.deltaTime;
            if (CurrentTIme >= 1)
            {
                CurrentTIme = 0;
                StateEnd();
            }
        }
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