using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerMove playerMove;
    public Animator animator;

    public void RollStart()
    {
        playerMove.isRoll = true;
        playerMove.speed = 15f;
        playerMove.playerData.rollStack--;
        playerMove.evadeSystem.enabled = true;
    }

    public void RollEnd()
    {
        playerMove.isRoll = false;
        playerMove.speed = 11f;
        playerMove.playerData.rollStack++;
        playerMove.evadeSystem.enabled = false;

        animator.StopPlayback();
        animator.SetBool("Roll", false);
        animator.speed = 1f;
    }

    public void SlowDown()
    {
        animator.speed = 2f;
        playerMove.speed = 6f;
    }
}