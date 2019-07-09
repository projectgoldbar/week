using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 11.0f;
    public float originMaxSpeed = 11.0f;
    public float speed = 0.0f;
    public float accelSpeed = 1f;
    public float downSpeed = 1f;
    public float slowSpeed = 0f;
    public float rollDownSpeedTime = 2f;//구르고 난후 느려지는 시간

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

    public ParticleSystem Portal = null;
    private AnimationEvents animEvent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        evadeMove = new Action[10] { () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { }, () => { } };
        evadeMove[0] = NomalRoll;
        evadeMove[1] = Rugby;
        evadeMove[2] = Chearleader;
        evadeMove[3] = Nurse;
        evadeMove[4] = Potal;
        evadeMove[5] = CeleryMan;
        evadeMove[6] = MotorCycle;
        evadeMove[7] = WormSkinEquipRolling;
        evadeMove[8] = EnergyShield;
        evadeMove[9] = NomalRoll;

        //animEvent = playerData.animator.GetComponent<AnimationEvents>();

        Portal.Stop();
    }

    private Vector2 bufferVector2;
    private Vector2 inputVector2;
    private Vector2 start;
    private float speedDistance;
    public bool CalamityRoll = false;
    public bool inputStey = false;
    private float distance;

    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
        int biteCount = playerData.biteZombies.Count;

        if (Input.GetMouseButtonDown(0))
        {
            //StartCoroutine(FadeIn(0));
            inputVector2 = Input.mousePosition;
            bufferVector2 = Input.mousePosition;
            Quaternion dir = Quaternion.LookRotation(inputVector2);
            transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * 10.0f);
            accel = true;
        }

        if (accel == true)
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
            else if (speed <= 0f)
            {
                speed = 0f;
                playerData.Hp = playerData.breathingHp * Time.deltaTime;
            }
        }

        if (Input.GetMouseButton(0))
        {
            distance = DisNdir(mousePosition, start).dis;
            speedDistance = DisNdir(mousePosition, bufferVector2).dis;
            var speed = speedDistance * 0.045f;

            if (speed > rollSensitive && !isRoll)
            {
                if (equipIdx == 2 && playerData.Hp > playerData.rollEp)
                {
                    Debug.Log("치어리더성공");
                    var dir = DisNdir(mousePosition, bufferVector2).dir;
                    Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
                    transform.rotation = Quaternion.LookRotation(rollDirection);
                    evadeMove[equipIdx]();
                    if (biteCount > 0)
                    {
                        for (int i = 0; i < playerData.biteZombies.Count;)
                        {
                            var x = playerData.biteZombies.Dequeue();
                            x.transform.parent = null;
                            x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
                        }
                    }
                }
                else if (equipIdx != 2 && playerData.ep > playerData.rollEp)
                {
                    Debug.Log("롤입력성공");
                    var dir = DisNdir(mousePosition, bufferVector2).dir;
                    Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
                    transform.rotation = Quaternion.LookRotation(rollDirection);
                    evadeMove[equipIdx]();
                    if (biteCount > 0)
                    {
                        for (int i = 0; i < playerData.biteZombies.Count;)
                        {
                            var x = playerData.biteZombies.Dequeue();
                            x.transform.parent = null;
                            x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
                        }
                    }
                }
            }
            else if (!isRoll)
            {
                var dir = DisNdir(mousePosition, inputVector2).dir;

                Vector3 moveDirection = new Vector3(dir.x, 0, dir.y);
                Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //StartCoroutine(FadeIn(1));
            accel = false;
        }
        bufferVector2 = Input.mousePosition;
        agent.velocity = agent.transform.forward * (speed - (slowSpeed + biteCount));

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
        playerData.ep -= playerData.rollEp;
    }

    //럭비
    private void Rugby()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void Chearleader()
    {
        playerData.animator.Play("Roll");
        playerData.Hp = -1 * playerData.rollEp;
    }

    private void Nurse()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    public bool potalOpen = false;

    private void Potal()
    {
        Portal.transform.position = transform.position + (transform.forward * 20);
        Portal.gameObject.SetActive(true);
        Portal.Stop();

        if (Portal.GetComponent<PortalEffectCollider>().is_Building)
        {
            playerData.animator.Play("Roll");
        }
        else
        {
            //포탈구르기
            playerData.animator.Play("portalOpen");
            potalOpen = true;
            accel = false;
        }

        playerData.ep -= playerData.rollEp;
    }

    private void CeleryMan()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void MotorCycle()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    public bool wormSkinEquipRolling;

    private void WormSkinEquipRolling()
    {
        playerData.animator.Play("Roll");
        wormSkinEquipRolling = true;

        playerData.ep -= playerData.rollEp;
    }

    private void EnergyShield()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void j()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void k()
    {
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }
}