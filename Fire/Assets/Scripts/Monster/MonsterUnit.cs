using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MonsterUnit : MonoBehaviour
{
    #region 변수들

    public float hp { get; set; }

    public float atkPoint;

    public StateIndex state = StateIndex.IDLE;

    public enum PatrolState { NONE, STURN, JUMP }

    public PatrolState patrolState = PatrolState.NONE;

    public RandomAngle random = new RandomAngle();

    public NavMeshAgent agent;

    private Animator Anim;

    public Action Patrol;

    private float TImer = 3.0f;

    [SerializeField]
    private float Distance = 0;

    private Action StateAction_Idle;
    private Action StateAction_Patroll;
    private Action StateAction_Chose;

    #endregion 변수들

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StateAction_Idle += StateIdle;
    }

    private void Start()
    {
        StartCoroutine(process());
        StartCoroutine(DistanceNStateCheck());
    }

    #region 딜레이

    private IEnumerator TimerDelay(Action action, float TImer)
    {
        float tmr = UnityEngine.Random.Range(1, TImer);
        yield return new WaitForSeconds(tmr);
        action?.Invoke();
    }

    #endregion 딜레이

    #region process

    private IEnumerator process()
    {
        StateAction_Idle?.Invoke();
        yield return null;
    }

    #endregion process

    #region 대기

    private void StateIdle()
    {
        StateAction_Idle -= StateIdle;
        StateAction_Patroll += StatePatrol;

        state = StateIndex.IDLE;
        StartCoroutine(State_Action());

        StartCoroutine(TimerDelay(StateAction_Patroll, TImer));
    }

    #endregion 대기

    #region 순찰

    private void StatePatrol()
    {
        StateAction_Idle += StateIdle;
        StateAction_Patroll -= StatePatrol;

        state = StateIndex.PATROL;
        StartCoroutine(State_Action());

        StartCoroutine(TimerDelay(StateAction_Idle, TImer));
    }

    #endregion 순찰

    #region 추적

    private void StateChase()
    {
        state = StateIndex.CHASE;
        StartCoroutine(State_Action());

        //StartCoroutine(TimerDelay(StateAction_Idle, TImer));
    }

    #endregion 추적

    #region Switch로 상태체크

    private IEnumerator State_Action()
    {
        switch (state)
        {
            case StateIndex.IDLE:
                //스탑
                Anim.SetBool("Walk", false);
                agent.stoppingDistance = 0;
                agent.destination = transform.position;             // 대기상태일떄 <이동불가>
                break;

            case StateIndex.PATROL:
                // 걷는 애니메이션
                Anim.SetBool("Walk", true);
                agent.stoppingDistance = 0;
                var randomPos = transform.position + random.RandomPosition();
                agent.destination = randomPos;
                break;

            case StateIndex.CHASE:
                Anim.SetBool("Walk", true);
                agent.stoppingDistance = 2;
                agent.destination = Ref.Instance.playerTr.position;
                break;

            case StateIndex.ATTACK:
                //공격 애니메이션 실행!
                break;
        }
        yield return null;
    }

    #endregion Switch로 상태체크

    private IEnumerator DistanceNStateCheck()
    {
        while (true)
        {
            Vector3 dir = (Ref.Instance.playerTr.position - transform.position);
            Distance = dir.magnitude;
            if (Distance < 2.0f)
            {
                state = StateIndex.ATTACK;
                StartCoroutine(State_Action());
            }
            else if (Distance < 10.0f)
            {
                state = StateIndex.CHASE;
                StartCoroutine(State_Action());
            }
            yield return null;
        }
    }
}

public class RandomAngle
{
    private float PatrolDistance = 5.0f;

    public float Value(float a, float b)
    {
        var value = UnityEngine.Random.Range(a, b);
        return value;
    }

    public Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();

        var angle = Value(0.0f, 360.0f);

        pos.x = Mathf.Cos(angle) * PatrolDistance;
        pos.y = 0;
        pos.z = Mathf.Sin(angle) * PatrolDistance;

        return pos;
    }
}