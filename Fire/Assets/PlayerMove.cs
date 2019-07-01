using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float speed = 11f;
    public DynamicJoystick dynamicJoystick;
    private NavMeshAgent agent;
    public PlayerData playerData;
    public bool isRoll = false;

    [Header("플레이어방향UI")]
    public Image center;

    public RectTransform knob;
    public float range = 1f;

    public float rollSensitive;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private Vector2 bufferVector2;
    private float speedDistance;

    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeIn(0));
        }

        if (Input.GetMouseButton(0))
        {
            speedDistance = DisNdir(mousePosition, bufferVector2).dis;
            var speed = speedDistance * 0.045f;
            if (speed > rollSensitive && !isRoll)
            {
                var dir = DisNdir(mousePosition, bufferVector2).dir;
                Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
                Debug.Log(rollDirection);
                transform.rotation = Quaternion.LookRotation(rollDirection);
                playerData.animator.Play("Roll");
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
        }
        bufferVector2 = Input.mousePosition;
        agent.velocity = agent.transform.forward * speed;
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

    public float accelspeed = 1f;

    private IEnumerator Accel(bool onoff)
    {
        float currentSpeed = 1f;
        while (true)
        {
        }
    }
}