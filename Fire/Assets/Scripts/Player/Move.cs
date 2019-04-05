using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

public class Move : MonoBehaviour
{
    public float runSpeed = 1000.0f;
    public float rotSpeed = 3.0f;
    public float damp = 20.0f;
    public static Action StopMove = () => { };

    private enum RotState { ADVANCE, LEFT, RIGHT, DEAD };

    [SerializeField]
    private RotState rotState = RotState.ADVANCE;

    private Rigidbody rid;

    private Vector2 touchPos = Vector2.zero;

    private Transform tr;

    private NavMeshAgent agent;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(MoveRotState());
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
        rid.velocity = tr.forward * runSpeed;
    }

    private void stopRun()
    {
        rid.velocity = Vector3.zero;
        StopMove -= stopRun;
    }

    private IEnumerator MoveRotState()
    {
        while (true)
        {
            switch (rotState)
            {
                case RotState.LEFT:
                    transform.rotation *= Quaternion.Euler(Vector3.up * Time.deltaTime * -rotSpeed * damp);
                    break;

                case RotState.RIGHT:
                    transform.rotation *= Quaternion.Euler(Vector3.up * Time.deltaTime * rotSpeed * damp);
                    break;

                case RotState.DEAD:
                    StopMove?.Invoke();
                    break;
            }

            yield return null;
        }
    }
}