using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 11f;
    public float rotSpeed = 300.0f;
    private Rigidbody rb;
    private Vector2 touchPos = Vector2.zero;
    private NavMeshAgent agent;
    private PlayerData playerData;

    public enum State { IDLE, ADVANCE, LEFT, RIGHT, DEAD, Special };

    private bool Left_flag = false;
    private bool Right_flag = false;

    public State rotState = State.ADVANCE;
    private Touch touch;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        playerData = GetComponent<PlayerData>();
    }

    private void Update()
    {
#if UNITY_EDITOR

        touchPos = Input.mousePosition;
        //if (EventSystem.current.IsPointerOverGameObject() == false)
        PcMove();
#else
        //if (EventSystem.current.IsPointerOverGameObject() == false)
            MobileMove();
#endif
    }

    public virtual void PcMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (touchPos.x <= Screen.width * 0.5)
            {
                Left_flag = true;
                rotState = State.LEFT;
                //Car_LeftTurn();
            }
            if (touchPos.x > Screen.width * 0.5)
            {
                Right_flag = false;
                rotState = State.RIGHT;
                //Car_RightTurn();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (rotState == State.LEFT)
                Left_flag = false;
            else if (rotState == State.RIGHT)
                Right_flag = false;

            rotState = State.ADVANCE;
        }
    }

    public virtual void MobileMove()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    if (touch.position.x <= Screen.width * 0.5)
                    {
                        rotState = State.LEFT;
                        Left_flag = true;
                    }
                    if (touch.position.x > Screen.width * 0.5)
                    {
                        rotState = State.RIGHT;
                        Right_flag = true;
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    if (rotState == State.LEFT)
                        Left_flag = false;
                    else if (rotState == State.RIGHT)
                        Right_flag = false;

                    if (rotState == State.Special)
                    {
                        Left_flag = false;
                        Right_flag = false;
                    }

                    rotState = State.ADVANCE;
                }
            }
        }
    }

    public void MoveState()
    {
        SelectState();
    }

    private void FixedUpdate()
    {
        MoveState();
    }

    public virtual void SelectState()
    {
        agent.velocity = agent.transform.forward * speed;

        if (Left_flag && Right_flag)
        {
            rotState = State.Special;
        }

        switch (rotState)
        {
            case State.ADVANCE:
                break;

            case State.LEFT:
                //if (!swipe.GoSwipe)
                Left_Turn();
                break;

            case State.RIGHT:
                //if (!swipe.GoSwipe)
                Right_Turn();
                break;

            case State.DEAD:
                Debug.Log("죽음");
                break;
        }
    }

    public void Left_Turn()
    {
        transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * -rotSpeed);
    }

    public void Right_Turn()
    {
        transform.rotation *= Quaternion.Euler(Vector3.up * Time.fixedDeltaTime * rotSpeed);
    }

    public void Skill()
    {
        StartCoroutine(SkillOn());
    }

    private IEnumerator SkillOn()
    {
        //carAudioNormal.Stop();
        //carAudioBoost.Play();

        //Manager.boostFlash.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        //Manager.boostFlash.SetActive(false);
        //Manager.boostVignette.SetActive(true);
        transform.localScale = transform.localScale * (1 + playerData.skillLv * 0.1f);
        //boostParticles.Play();

        speed *= 2f;

        yield return new WaitForSeconds((2f + playerData.skillLv * 0.1f));

        transform.localScale = new Vector3(1f, 1f, 1f);

        speed = 11f;
        //boostParticles.Stop();
        //carAudioBoost.Stop();
        //carAudioNormal.Play();
    }
}