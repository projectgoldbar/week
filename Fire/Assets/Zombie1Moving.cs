using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieState;

public class Zombie1Moving : SpeedZomMoving
{

    public CameraView ZombieCameraView;


    [Header("플레이어 속도")]
    public float PlayerSpeed = 10;

    public float ViewOutSpeed = 20;


    public float RunnerTimer = 0;
    private float RunnerMaxTimer = 5.0f;

    public bool Running = false;


    private void OnEnable()
    {
        sturnCollider.SturnEvent += SturnChange;
        Trail.SetActive(false);
    }

    public override void Execute()
    {
        zombieData.animator.SetLayerWeight(1, 1);
        MoveingAnimChange = false;
        zombieData.agent.acceleration = 13f;
        SturnColl.enabled = false;

        PlayerSpeed = player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;

        if(Running)
            CurrentSpeed = PlayerSpeed;
        else
            CurrentSpeed = ViewOutSpeed;

        zombieData.agent.speed = CurrentSpeed;
        StartCoroutine(zombieData.moveCoroutine);
    }


    public override void Update()
    {
        if (ZombieCameraView.MoveSpeedChange)
        {
            RunnerTimer += Time.deltaTime;

            if (RunnerTimer >= RunnerMaxTimer && MaxSpeed > CurrentSpeed)
            {
                Running = true;
                RunnerTimer = 0;
                CurrentSpeed += AddSpeed;
            }

            if (MaxSpeed <= CurrentSpeed)
            {
                MoveingAnimChange = true;
                zombieData.animator.SetFloat("Speed", CurrentSpeed);
                SturnColl.enabled = true;
                Trail.SetActive(true);
                
            }

            zombieData.agent.speed = CurrentSpeed;
        }
    }

    public override void SturnChange()
    {
        if (MoveingAnimChange)
        {
            
            RunnerTimer = 0;
            StateChange(zombieData.stun);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }


}
