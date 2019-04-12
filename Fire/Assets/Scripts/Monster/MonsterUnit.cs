using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterUnit : MonoBehaviour
{
    #region 변수들

    public float atkPoint = 1;

    public float Distance = 0;

    public Transform Righthand;

    // [System.NonSerialized]
    public bool Check = false;

    // [System.NonSerialized]
    public bool Attack = false;

    public Animator Anim;
    public MonsterState monsterstate;

    public StateIndex state = StateIndex.PATROL;

    public Transform ChaseTarget = null;

    private float disCheck = 20.0f;

    #endregion 변수들

    private void Start()
    {
        StartCoroutine(DistanceNStateCheck());
    }

    //거리x
    //거리체크

    private IEnumerator DistanceNStateCheck()
    {
        ChaseTarget = Ref.Instance.playerTr;
        while (true)
        {
            if (ChaseTarget != null)
            {
                Vector3 dir = (ChaseTarget.position - transform.position);
                Distance = dir.magnitude;
            }

            PatrolCheck();

            ChaseCheck();

            // AttackCheck();

            yield return null;
        }
    }

    private void ChaseCheck()
    {
        if (Distance < disCheck && !Check)
        {
            Check = true;
        }
    }

    private void AttackCheck() //어택 2종류로 변경해야됨. 손 공격 / 돌진공격
    {
        if (Check)
        {
            if (Distance <= 3.5f)
                Attack = true;
        }
    }

    private void PatrolCheck()
    {
        if (Distance > disCheck && Check)
        {
            Check = false;
        }
    }

    //애니메이션 이벤트
    public void ShaseProcess()
    {
        if (Check)
            monsterstate.ChangeState(StateIndex.CHASE);
        else
            monsterstate.ChangeState(StateIndex.PATROL);
    }
}