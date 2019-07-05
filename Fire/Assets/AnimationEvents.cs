﻿using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerMove playerMove;
    public Animator animator;
    public float evadeSpeed = 20f;

    public void RollStart()
    {
        playerMove.isRoll = true;
        playerMove.maxSpeed = evadeSpeed;
        playerMove.speed = evadeSpeed;
        playerMove.playerData.rollStack--;
        playerMove.evadeSystem.enabled = true;
    }

    public void RollEnd()
    {
        playerMove.isRoll = false;
        playerMove.maxSpeed = 11f;
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
        playerMove.maxSpeed = 6f;
        playerMove.speed = 6f;
    }

    public void DieStart()
    {
        playerMove.enabled = false;
        FindObjectOfType<UITweenEffectManager>().LeaveInGame();
    }

    public void GameOver()
    {
        FindObjectOfType<Manager>().GameOver();
    }
}