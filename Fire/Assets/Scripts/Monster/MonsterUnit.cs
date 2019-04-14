using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterUnit : MonoBehaviour
{
    #region 변수들

    public float atkPoint = 1;

    public float Distance = 0;

    // [System.NonSerialized]
    public bool Check = false;

    // [System.NonSerialized]
    public bool Attack = false;

    public Animator Anim;
    public MonsterState monsterstate;

    public StateIndex state = StateIndex.PATROL;

    public Transform ChaseTarget = null;

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

            yield return null;
        }
    }
}