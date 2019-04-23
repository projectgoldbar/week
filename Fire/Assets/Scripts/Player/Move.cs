using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

public class Move : MonoBehaviour
{
    public float runSpeed = 100.0f;

    public float rotSpeed = 300.0f;

    public static Action StopMove = () => { };

    public enum State { IDLE, ADVANCE, LEFT, RIGHT, DEAD };

    [SerializeField]
    public State rotState = State.ADVANCE;

    protected Vector2 touchPos = Vector2.zero;

    protected NavMeshAgent agent;

    private Touch touch;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (touchPos.x <= Screen.width * 0.5)
                {
                    rotState = State.LEFT;
                    Car_LeftTurn();
                }
                else
                {
                    rotState = State.RIGHT;
                    Car_RightTurn();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                rotState = State.ADVANCE;
            }
        }

#else
        //if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    touch = Input.GetTouch(i);

                    if (touch.phase == TouchPhase.Began)
                    {
                        if (touch.position.x <= Screen.width * 0.5)
                        {
                            rotState = State.LEFT;
                        }
                        else
                        {
                            rotState = State.RIGHT;
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        rotState = State.ADVANCE;
                    }
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
        switch (rotState)
        {
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