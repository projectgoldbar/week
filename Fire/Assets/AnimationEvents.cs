﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Joystick joystick;
    public JoyStick2 joystick2;
    public Animator animator;

    public void RollStart()
    {
    }

    public void RollEnd()
    {
        if (!joystick.enabled)
        {
            joystick2.isRoll = false;
            joystick2.MoveSpeed = 11f;
        }
        else
        {
            joystick.isRoll = false;
            joystick.MoveSpeed = 11f;
        }

        animator.StopPlayback();
        animator.SetBool("Roll", false);
        animator.speed = 1f;
    }

    public void SlowDown()
    {
        animator.speed = 2f;
        if (!joystick.enabled)
        {
            joystick2.MoveSpeed = 6f;
        }
        else
        {
            joystick.MoveSpeed = 6f;
        }
    }
}