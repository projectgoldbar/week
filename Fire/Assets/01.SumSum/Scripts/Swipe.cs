using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Swipe : MonoBehaviour
{
    public enum Mode
    { move, roll }

    public Vector2 StartPos;
    public Vector2 CurrentPos;
    public Vector2 pos;
    public float swipeLength = 120f;
    public Mode mode = Mode.roll;

    [Header("Player")]
    public Transform Target = null;

    private NavMeshAgent agent;
    public float MoveSpeed = 11;

    [Header("구르기 기능(함수) 연결")]
    public UnityEvent roll;

    private void Awake()
    {
        agent = Target.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ClickSet();

        SwipeGo();

        agent.velocity = agent.transform.forward * MoveSpeed;
    }

    private void ClickSet()
    {
        if (Input.GetMouseButtonDown(0))
            StartPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
            CurrentPos = Input.mousePosition;

        pos = (CurrentPos - StartPos);
    }

    private void SwipeGo()
    {
        var SwipeDistance = pos.magnitude;

        Debug.Log(SwipeDistance);
        if (SwipeDistance >= swipeLength)
        {
            switch (mode)
            {
                case Mode.move:
                    var direction = pos.normalized;
                    Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

                    Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
                    Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, Time.deltaTime * 10.0f);
                    break;

                case Mode.roll:
                    direction = pos.normalized;
                    moveDirection = new Vector3(direction.x, 0, direction.y);
                    targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
                    Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, Time.deltaTime * 10.0f);
                    MoveSpeed = 20f;

                    break;

                default:
                    break;
            }

            roll?.Invoke();
        }
    }

    private void MoveWithSwipe()
    {
    }

    private void RollWithSwipe()
    {
    }
}