﻿using UnityEngine;
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
    public float rollSensitive;

    public bool accel = false;
    public int equipIdx = 0;

    public ParticleSystem Portal = null;
    public AnimationEvents animEvent;

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

    #region 상현이 코드 추가

    public RectTransform center;
    public RectTransform knob;
    public Vector2 startPos;
    public bool evadeRotate = false;
    public Vector2 direction;
    public float range = 1f;
    private float distance;

    private void Start()
    {
        startPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        knob.position = startPos;
        center.position = startPos;
    }

    #endregion 상현이 코드 추가

    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        //Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
        int biteCount = playerData.biteZombies.Count;

        if (Input.GetMouseButtonDown(0))
        {
            //StartCoroutine(FadeIn(0));
            inputVector2 = Input.mousePosition;
            //bufferVector2 = Input.mousePosition;
            Quaternion dir = Quaternion.LookRotation(inputVector2);
            transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * 10.0f);
            knob.position = mousePosition;
            center.position = mousePosition;
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
            knob.position = mousePosition;
            knob.position = center.position + Vector3.ClampMagnitude(knob.position - center.position, center.sizeDelta.x * range);

            if (knob.position != Input.mousePosition)
            {
                Vector3 outsideBoundsVector = Input.mousePosition - knob.position;
                //center.position += outsideBoundsVector;
            }
            direction = DisNdir(knob.position, center.position).dir;
            distance = DisNdir(mousePosition, start).dis;
            //distance = DisNdir(mousePosition, start).dis;
            //speedDistance = DisNdir(mousePosition, bufferVector2).dis;
            //var speed = speedDistance * 0.045f;

            //if (speed > rollSensitive && !isRoll)
            //{
            //    if (equipIdx == 2 && playerData.Hp > playerData.rollEp)
            //    {
            //        Debug.Log("치어리더성공");
            //        var dir = DisNdir(mousePosition, bufferVector2).dir;
            //        Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
            //        transform.rotation = Quaternion.LookRotation(rollDirection);
            //        evadeMove[equipIdx]();
            //        if (biteCount > 0)
            //        {
            //            for (int i = 0; i < playerData.biteZombies.Count;)
            //            {
            //                var x = playerData.biteZombies.Dequeue();
            //                x.transform.parent = null;
            //                x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
            //            }
            //        }
            //    }
            //    else if (equipIdx != 2 && playerData.ep > playerData.rollEp)
            //    {
            //        Debug.Log("롤입력성공");
            //        var dir = DisNdir(mousePosition, bufferVector2).dir;
            //        Vector3 rollDirection = new Vector3(dir.x, 0, dir.y);
            //        transform.rotation = Quaternion.LookRotation(rollDirection);

            //        evadeMove[equipIdx]();

            //        if (biteCount > 0)
            //        {
            //            for (int i = 0; i < playerData.biteZombies.Count;)
            //            {
            //                var x = playerData.biteZombies.Dequeue();
            //                x.transform.parent = null;
            //                x.GetComponent<ZombieState.Zombie_Bite>().ZombieDown();
            //            }
            //        }
            //    }
        }
        if (distance < 20) return;
        if (!isRoll)
        {
            //var dir = DisNdir(mousePosition, inputVector2).dir;

            //Vector3 moveDirection = new Vector3(dir.x, 0, dir.y);
            //Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            //Target.rotation = targetRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //StartCoroutine(FadeIn(1));
            accel = false;
            knob.position = start;
        }
        agent.velocity = agent.transform.forward * (speed - (slowSpeed + biteCount));
        playerData.animator.SetFloat("moveSpeed", speed);

        //bufferVector2 = Input.mousePosition;
    }

    public (float dis, Vector3 dir) DisNdir(Vector3 aa, Vector3 bb)
    {
        var Init = (aa - bb);

        var dir = Init.normalized;
        var dis = Init.magnitude;

        return (dis, dir);
    }

    //-------------------- 복장입어서 추가되는 동작들------

    public Action[] evadeMove;

    private void NomalRoll()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    //럭비
    private void Rugby()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        animEvent.evadeSpeed = 25f;
        playerData.animator.Play("Spin");
        playerData.ep -= playerData.rollEp;
    }

    private void Chearleader()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.Hp = -1 * playerData.rollEp;
    }

    private void Nurse()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
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
            SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
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
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void MotorCycle()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    public bool wormSkinEquipRolling;

    private void WormSkinEquipRolling()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        wormSkinEquipRolling = true;

        playerData.ep -= playerData.rollEp;
    }

    private void EnergyShield()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void j()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }

    private void k()
    {
        SoundManager.Instance.PlaySoundSFX("ROLLINGPLAYER");
        playerData.animator.Play("Roll");
        playerData.ep -= playerData.rollEp;
    }
}