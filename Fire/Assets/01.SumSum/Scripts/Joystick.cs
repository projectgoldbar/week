using UnityEngine;
using UnityEngine.AI;

public class Joystick : MonoBehaviour {

    public RectTransform center;
    public RectTransform knob;
    public float range;
    public bool fixedJoystick;

    [HideInInspector]
    public Vector2 direction;

    Vector2 start;

    [Header("Player")]
    public Transform Target = null;
    NavMeshAgent agent;

    public Vector2 StartPos;

    public float MoveSpeed = 11;

    private float distance;


    public bool JoyStickMoving = false;

    private void Awake()
    {
        agent = Target.GetComponent<NavMeshAgent>();
    }

    void Start() {
        ShowHide(true);
        StartPos = new Vector2(Screen.width * 0.5f, 200.0f);
        knob.position = StartPos;
        center.position = StartPos;
    }

    void Update() {
        Vector2 pos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0)) {
            //ShowHide(true);

            start = pos;

            knob.position = pos;
            center.position = pos;
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

            direction   = DisNdir(knob.position ,center.position).dir;
            distance    = DisNdir(pos, start).dis;

            //Debug.Log(direction);
            //Debug.Log($"마우스 초기위치부터 현재위치까지의 거리{distance}");

            if (distance < 20) return;
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            //Target.rotation = targetRotation;
            Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, Time.deltaTime * 10.0f);

            if(JoyStickMoving)
            agent.velocity = agent.transform.forward * MoveSpeed;
        }
        else if (Input.GetMouseButtonUp(0)) {
            //ShowHide(false);
            direction = Vector2.zero;
            knob.position = start;
            center.position = start;

            if (JoyStickMoving)
                agent.velocity = Vector3.zero;
        }

        if (!JoyStickMoving) agent.velocity = agent.transform.forward * MoveSpeed;


    }

    void ShowHide(bool state) {
        center.gameObject.SetActive(state);
        knob.gameObject.SetActive(state);
    }

    public (float dis , Vector3 dir) DisNdir(Vector3 aa, Vector3 bb)
    {
        var Init = (aa - bb);

        var dir = Init.normalized;
        var dis = Init.magnitude;

        return (dis, dir);
    }
}