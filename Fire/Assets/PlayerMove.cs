using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 11.0f;
    public float speed = 0.0f;
    public float accelSpeed = 1f;
    public float downSpeed = 1f;

    public DynamicJoystick dynamicJoystick;
    private NavMeshAgent agent;
    public PlayerData playerData;
    public EvadeSystem evadeSystem;
    public bool isRoll = false;

    [Header("플레이어방향UI")]
    public Image center;

    public RectTransform knob;
    public float range = 1f;
    public float rollSensitive;
    public bool accel = false;
    public int equipIdx = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        evadeMove = new Action[10] { () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { } };
        evadeMove[0] = NomalRoll;
        evadeMove[1] = NomalRoll;
        evadeMove[2] = NomalRoll;
        evadeMove[3] = NomalRoll;
        evadeMove[4] = NomalRoll;
        evadeMove[5] = NomalRoll;
        evadeMove[6] = NomalRoll;
        evadeMove[7] = NomalRoll;
        evadeMove[8] = NomalRoll;
        evadeMove[9] = NomalRoll;
    }

    private Vector2 bufferVector2;
    private float speedDistance;
    public bool CalamityRoll = false;

    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeIn(0));
            bufferVector2 = Input.mousePosition;
            accel = true;
        }

        if (accel)
        {
            if (maxSpeed > speed)
            {
                speed += accelSpeed;
            }
            else
            {
                speed = maxSpeed;
            }
        }
        else
        {
            if (0f < speed && !isRoll)
            {
                speed -= downSpeed;
            }
            else if (speed < 0f)
            {
                speed = 0f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            speedDistance = DisNdir(mousePosition, bufferVector2).dis;
            var speed = speedDistance * 0.045f;
            if (speed > rollSensitive && !isRoll && playerData.ep > 6f)
            {
                var dir = DisNdir(mousePosition, bufferVector2).dir;
                Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
                transform.rotation = Quaternion.LookRotation(rollDirection);
                evadeMove[equipIdx]();
            }
            else if (!isRoll)
            {
                Quaternion dir = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * 10.0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(FadeIn(1));
            accel = false;
        }
        bufferVector2 = Input.mousePosition;
        agent.velocity = agent.transform.forward * speed;

        playerData.animator.SetFloat("moveSpeed", speed);
    }

    public (float dis, Vector3 dir) DisNdir(Vector3 aa, Vector3 bb)
    {
        var Init = (aa - bb);

        var dir = Init.normalized;
        var dis = Init.magnitude;

        return (dis, dir);
    }

    private IEnumerator FadeIn(int c)
    {
        Color color = new Color(0, 0, 0, 0.01f);
        for (int i = 0; i < 45; i++)
        {
            if (c == 0)
            {
                center.color += color;
                yield return null;
            }
            else
            {
                center.color -= color;
                yield return null;
            }
        }
        yield break;
    }

    //-------------------- 복장입어서 추가되는 동작들------

    public Action[] evadeMove;

    private void NomalRoll()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void b()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void c()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void d()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void e()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void f()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void g()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void h()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void i()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void j()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }

    private void k()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= 7f;
    }
}