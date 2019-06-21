using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Swipe : MonoBehaviour
{
    public Vector2 StartPos;
    public Vector2 CurrentPos;
    public Vector2 pos;

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

    void Update()
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

        if (SwipeDistance >= 120.0f)
        {
            var direction = pos.normalized;
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, Time.deltaTime * 10.0f);

            roll?.Invoke();
        }
    }





}
