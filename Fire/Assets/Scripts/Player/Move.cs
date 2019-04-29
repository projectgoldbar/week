using System;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [NonSerialized]
    public float runSpeed = 100.0f;

    public float rotSpeed = 300.0f;

    public static Action StopMove = () => { };

    public enum State { IDLE, ADVANCE, LEFT, RIGHT, DEAD, Special };

    [SerializeField]
    public State rotState = State.ADVANCE;

    protected Vector2 touchPos = Vector2.zero;

    [NonSerialized]
    public NavMeshAgent agent;

    private Touch touch;

    private bool Left_flag = false;
    private bool Right_flag = false;

    private bool b_Touch = false;

    private Swipe swipe;

    public Rigidbody rid;

    public void Awake()
    {
        rid = GetComponent<Rigidbody>();
        swipe = FindObjectOfType<Swipe>();
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
        PcMove();
#else
        //if (EventSystem.current.IsPointerOverGameObject() == false)
            MobileMove();
#endif
    }

    public virtual void PcMove()
    {
        if (Input.GetMouseButtonDown(0) && (!swipe.GoSwipe))
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
        else if (Input.GetMouseButtonUp(0) && (!swipe.GoSwipe))
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

    public virtual void MobileMove()
    {
        if (Input.touchCount > 0 && (!swipe.GoSwipe))
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

    public bool rotationStop = false;

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
                agent.speed = 11;
                break;

            case State.Special:
                agent.speed += Time.deltaTime * 10;
                break;

            case State.LEFT:
                //if (!swipe.GoSwipe)
                Left_Turn();
                break;

            case State.RIGHT:
                //if (!swipe.GoSwipe)
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
        swipe.GoSwipe = true;
        agent.speed = 0;
        StopMove -= stopRun;
    }
}