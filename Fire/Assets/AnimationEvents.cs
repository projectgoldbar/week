using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public JoyStick2 joystick2;
    public Animator animator;

    public void RollStart()
    {
        joystick2.isRoll = true;
        joystick2.MoveSpeed = 15f;
        joystick2.playerData.rollStack--;
    }

    public void RollEnd()
    {
        joystick2.isRoll = false;
        joystick2.MoveSpeed = 11f;
        joystick2.playerData.rollStack++;

        animator.StopPlayback();
        animator.SetBool("Roll", false);
        animator.speed = 1f;
    }

    public void SlowDown()
    {
        animator.speed = 2f;
        joystick2.MoveSpeed = 6f;
    }
}