using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

public class Move : MonoBehaviour
{
    public float runSpeed = 100.0f;
    public float rotSpeed = 100.0f;

    public static Action StopMove = () => { };

    public enum State { IDLE, ADVANCE, LEFT, RIGHT, DEAD };

    [SerializeField]
    public State rotState = State.ADVANCE;

    protected Vector2 touchPos = Vector2.zero;

    private NavMeshAgent agent;

    private void Awake()
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
                    // Car_LeftTurn();
                }
                else
                {
                    rotState = State.RIGHT;
                    // Car_RightTurn();
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
                touchPos = Input.GetTouch(0).position;
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                     if (touchPos.x <= Screen.width * 0.5)
                    {
                       rotState = State.LEFT;
                    }
                    else
                    {
                        rotState = State.RIGHT;
                    }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                   rotState = State.ADVANCE;
                }
            }
        }
#endif
    }

    private void FixedUpdate()
    {
        runSpeed = agent.speed;
        agent.velocity = agent.transform.forward * runSpeed;

        MoveState();
    }

    public void MoveState()
    {
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

    private void stopRun()
    {
        agent.velocity = Vector3.zero;
        StopMove -= stopRun;
    }
}