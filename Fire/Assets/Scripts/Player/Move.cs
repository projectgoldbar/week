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

    private enum RotState { ADVANCE, LEFT, RIGHT, DEAD };

    [SerializeField]
    private RotState rotState = RotState.ADVANCE;

    private Vector2 touchPos = Vector2.zero;

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
                    rotState = RotState.LEFT;
                }
                else
                {
                    rotState = RotState.RIGHT;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                rotState = RotState.ADVANCE;
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
                       rotState = RotState.LEFT;
                    }
                    else
                    {
                        rotState = RotState.RIGHT;
                    }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                   rotState = RotState.ADVANCE;
                }
            }
        }
#endif
    }

    private void FixedUpdate()
    {
        runSpeed = agent.speed;
        agent.velocity = agent.transform.forward * runSpeed;

        switch (rotState)
        {
            case RotState.LEFT:

                agent.transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * -rotSpeed);
                break;

            case RotState.RIGHT:
                agent.transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * rotSpeed);
                break;

            case RotState.DEAD:
                StopMove?.Invoke();
                break;
        }
    }

    private void stopRun()
    {
        agent.velocity = Vector3.zero;
        StopMove -= stopRun;
    }

    private IEnumerator MoveRotState()
    {
        while (true)
        {
            switch (rotState)
            {
                case RotState.LEFT:

                    transform.rotation *= Quaternion.Euler(Vector3.up * Time.deltaTime * -rotSpeed);
                    break;

                case RotState.RIGHT:
                    transform.rotation *= Quaternion.Euler(Vector3.up * Time.deltaTime * rotSpeed);
                    break;

                case RotState.DEAD:
                    StopMove?.Invoke();
                    break;
            }

            yield return null;
        }
    }
}