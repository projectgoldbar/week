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

    public Text tt;

    private bool b_Touch = false;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(Mover());
    }

    private void OnEnable()
    {
        StopMove += stopRun;
    }

    private void Update()
    {
        #region UNITY_EDITOR Code

        //#if UNITY_EDITOR

        //        touchPos = Input.mousePosition;
        //        //if (EventSystem.current.IsPointerOverGameObject() == false)
        //        {
        //            if (Input.GetMouseButtonDown(0))
        //            {
        //                tt.text = "PC";
        //                if (touchPos.x <= Screen.width * 0.5)
        //                {
        //                    Left_flag = true;
        //                    rotState = State.LEFT;
        //                    //Car_LeftTurn();
        //                }
        //                if (touchPos.x > Screen.width * 0.5)
        //                {
        //                    Right_flag = false;
        //                    rotState = State.RIGHT;
        //                    //Car_RightTurn();
        //                }
        //            }
        //            else if (Input.GetMouseButtonUp(0))
        //            {
        //                if (rotState == State.LEFT)
        //                    Left_flag = false;
        //                else if (rotState == State.RIGHT)
        //                    Right_flag = false;

        //                if (rotState == State.Special)
        //                {
        //                    Left_flag = false;
        //                    Right_flag = false;
        //                }
        //                rotState = State.ADVANCE;
        //            }
        //        }
        //#else

        #endregion UNITY_EDITOR Code

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

        //#endif
    }

    private IEnumerator Mover()
    {
        while (true)
        {
            MoveState();
            yield return null;
        }
    }

    public virtual void Car_LeftTurn()
    {
    }

    public virtual void Car_RightTurn()
    {
    }

    public void MoveState()
    {
        SelectState();
    }

    public virtual void SelectState()
    {
        if (Left_flag && Right_flag)
        {
            rotState = State.Special;
        }

        switch (rotState)
        {
            case State.ADVANCE:
                tt.text = agent.speed.ToString();
                agent.speed = 30;
                break;

            case State.Special:
                tt.text = agent.speed.ToString() + "   ~~~~~스페셜무브";
                agent.speed += Time.deltaTime * 10;
                break;

            case State.LEFT:
                tt.text = agent.speed.ToString() + "좌회전";
                Left_Turn();
                break;

            case State.RIGHT:
                tt.text = agent.speed.ToString() + "우회전";
                Right_Turn();
                break;

            case State.DEAD:
                StopMove?.Invoke();
                break;
        }

        runSpeed = agent.speed;
        agent.velocity = agent.transform.forward * runSpeed;
    }

    public void Left_Turn()
    {
        agent.transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * -rotSpeed);
    }

    public void Right_Turn()
    {
        agent.transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * rotSpeed);
    }

    public virtual void stopRun()
    {
        agent.speed = 0;
        StopMove -= stopRun;
    }
}