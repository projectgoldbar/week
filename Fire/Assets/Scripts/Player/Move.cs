using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class Move : MonoBehaviour
{
    public float runSpeed = 100.0f;

    public float rotSpeed = 300.0f;

    public static Action StopMove = () => { };

    public enum State { IDLE, ADVANCE, LEFT, RIGHT, DEAD, Special };

    [SerializeField]
    public State rotState = State.ADVANCE;

    protected Vector2 touchPos = Vector2.zero;

    protected NavMeshAgent agent;

    private Touch touch;

    private bool Left_flag = false;
    private bool Right_flag = false;

    private bool b_Touch = false;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //StartCoroutine(Mover());
    }

    private void OnEnable()
    {
        StopMove += stopRun;
    }

    private void Update()
    {
#if UNITY_EDITOR

        touchPos = Input.mousePosition;
        //if (EventSystem.current.IsPointerOverGameObject() == false)

        if (Input.GetMouseButtonDown(0))
        {
            if (touchPos.x <= Screen.width * 0.5)
            {
                Left_flag = true;
                rotState = State.LEFT;
                //Car_LeftTurn();
            }
            if (touchPos.x > Screen.width * 0.5)
            {
                Right_flag = false;
                rotState = State.RIGHT;
                //Car_RightTurn();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (rotState == State.LEFT)
                Left_flag = false;
            else if (rotState == State.RIGHT)
                Right_flag = false;

            if (rotState == State.Special)
            {
                Left_flag = false;
                Right_flag = false;
            }
            rotState = State.ADVANCE;
        }

#else

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    if (touch.position.x <= Screen.width * 0.5)
                    {
                        rotState = State.LEFT;
                        Left_flag = true;
                    }
                    if (touch.position.x > Screen.width * 0.5)
                    {
                        rotState = State.RIGHT;
                        Right_flag = true;
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    if (rotState == State.LEFT)
                        Left_flag = false;
                    else if (rotState == State.RIGHT)
                        Right_flag = false;

                    if (rotState == State.Special)
                    {
                        Left_flag = false;
                        Right_flag = false;
                    }

                    rotState = State.ADVANCE;
                }
            }
        }

#endif
    }

    public virtual void Car_LeftTurn()
    {
    }

    public virtual void Car_RightTurn()
    {
    }

    private void FixedUpdate()
    {
        MoveState();
    }

    public void MoveState()
    {
        SelectState();
    }

    public virtual void SelectState()
    {
        runSpeed = agent.speed;
        agent.velocity = agent.transform.forward * runSpeed;
        if (Left_flag && Right_flag)
        {
            rotState = State.Special;
        }

        switch (rotState)
        {
            case State.ADVANCE:
                agent.speed = 30;
                break;

            case State.Special:
                agent.speed += Time.deltaTime * 10;
                break;

            case State.LEFT:
                Left_Turn();
                break;

            case State.RIGHT:
                Right_Turn();
                break;

            case State.DEAD:
                StopMove?.Invoke();
                break;
        }
    }

    public void Left_Turn()
    {
        transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * -rotSpeed);
    }

    public void Right_Turn()
    {
        transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * rotSpeed);
    }

    public virtual void stopRun()
    {
        agent.speed = 0;
        StopMove -= stopRun;
    }
}