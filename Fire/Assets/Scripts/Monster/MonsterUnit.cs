using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterUnit : MonoBehaviour
{
    #region 변수들

    public int level = 0;
    public float atkPoint = 1;

    public float Distance = 0;

    public Animator Anim;
    public MonsterState monsterstate;

    public StateIndex state = StateIndex.CHASE;

    public Transform ChaseTarget = null;

    public ZombieType type;

    private pomulseon pomulseon;

    private bool SearchIn = false;

    #endregion 변수들

    private void Start()
    {
        StartCoroutine(DistanceNStateCheck());
    }

    public void SetSearchIn(bool Search)
    {
        SearchIn = Search;
    }

    public void SetCatchTr()
    {
    }

    private void Awake()
    {
        pomulseon = GetComponent<pomulseon>();
    }

    public void FlyUp()
    {
        pomulseon.FlyToTarget(
            transform,
            transform.position,
            transform.position + transform.forward * 10,
            (tr) => FlyEnd());
    }

    public void FlyEnd()
    {
        monsterstate.Agent.enabled = true;
        Anim.Play("Run");
    }

    //거리x
    //거리체크

    private IEnumerator DistanceNStateCheck()
    {
        ChaseTarget = Utility.Instance.playerTr;
        while (true)
        {
            if (ChaseTarget != null)
            {
                Vector3 dir = (ChaseTarget.position - transform.position);
                Distance = dir.magnitude;
            }

            yield return null;
        }
    }
}