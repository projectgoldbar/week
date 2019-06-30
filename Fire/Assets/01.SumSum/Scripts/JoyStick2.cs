using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class JoyStick2 : MonoBehaviour
{
    public RectTransform center;
    public RectTransform knob;
    public float range;
    public bool fixedJoystick;
    public float rollSensitive = 10f;
    public bool isRoll = false;

    [HideInInspector]
    public Vector2 direction;

    private Vector2 start;

    [Header("Player")]
    public Transform Target = null;

    public PlayerData playerData;

    private NavMeshAgent agent;

    public Vector2 startPos;

    public float MoveSpeed = 11;
    public bool evadeRotate = false;
    private float distance;

    public bool JoyStickMoving = true;

    private void Awake()
    {
        agent = Target.GetComponent<NavMeshAgent>();
        playerData = Target.GetComponent<PlayerData>();
    }

    private void Start()
    {
        ShowHide(true);
        startPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        knob.position = startPos;
        center.position = startPos;
    }

    public InputField InputField;

    public void SenceChange()
    {
        var x = InputField.text;
        var s = Convert.ToSingle(x);
        rollSensitive = s;
    }

    private int frame = 0;
    public float speedDistance = 0f;
    private Vector2 bufferVector2;

    private void Update()
    {
        //if (isRoll)
        //{
        //    Debug.Log("구르는중" + frame);
        //    frame++;

        //    return;
        //}

        Vector2 pos = Input.mousePosition;
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowHide(true);

                //start = pos;

                knob.position = pos;
                center.position = startPos;
            }
            else if (Input.GetMouseButton(0))
            {
                knob.position = pos;
                knob.position = center.position + Vector3.ClampMagnitude(knob.position - center.position, center.sizeDelta.x * range);

                if (knob.position != Input.mousePosition && !fixedJoystick)
                {
                    Vector3 outsideBoundsVector = Input.mousePosition - knob.position;
                    //center.position += outsideBoundsVector;
                }

                direction = DisNdir(knob.position, center.position).dir;
                distance = DisNdir(pos, start).dis;
                speedDistance = DisNdir(pos, bufferVector2).dis;

                var speed = speedDistance * 0.045;
                //Debug.Log("방향" + direction);
                //Debug.Log("거리" + distance);
                //Debug.Log("속력" + speed);

                //Debug.Log(direction);
                //Debug.Log($"마우스 초기위치부터 현재위치까지의 거리{distance}");

                if (speed > rollSensitive && playerData.rollStack > 0)
                {
                    Debug.Log("구른다");
                    Vector3 rollDirection = new Vector3(direction.x, 0, direction.y);
                    Quaternion playerRotation = rollDirection != Vector3.zero ? Quaternion.LookRotation(rollDirection) : transform.rotation;
                    Target.rotation = playerRotation = rollDirection != Vector3.zero ? Quaternion.LookRotation(rollDirection) : transform.rotation;
                    ;
                    playerData.animator.StopPlayback();
                    playerData.animator.Play("Roll");
                }

                if (distance < 20) return;
                if (!isRoll)
                {
                    Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

                    Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
                    //Target.rotation = targetRotation;
                    Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, Time.deltaTime * 10.0f);
                }
                if (JoyStickMoving)
                    agent.velocity = agent.transform.forward * MoveSpeed;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ShowHide(false);
                direction = Vector2.zero;
                knob.position = start;
                //center.position = start;

                if (JoyStickMoving)
                    agent.velocity = Vector3.zero;
            }

            if (!JoyStickMoving) agent.velocity = agent.transform.forward * MoveSpeed;
        }
        bufferVector2 = Input.mousePosition;
    }

    private void ShowHide(bool state)
    {
        center.gameObject.SetActive(state);
        knob.gameObject.SetActive(state);
    }

    public (float dis, Vector3 dir) DisNdir(Vector3 aa, Vector3 bb)
    {
        var Init = (aa - bb);

        var dir = Init.normalized;
        var dis = Init.magnitude;

        return (dis, dir);
    }
}