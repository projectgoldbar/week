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


    private float RunnerTimer = 0;
    private float RunnerMaxTimer = 5.0f;


    private void OnEnable()
    {
        Trail.SetActive(false);
    }

    public override void Execute()
    {
        zombieData.animator.SetLayerWeight(1, 1);

        zombieData.agent.acceleration = 13f;
        SturnColl.enabled = false;

        PlayerSpeed = player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;

        if (ZombieCameraView.MoveSpeedChange) CurrentSpeed = PlayerSpeed;
        else CurrentSpeed = ViewOutSpeed; 

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
                RunnerTimer = 0;
                CurrentSpeed += AddSpeed;
            }

            if (MaxSpeed <= CurrentSpeed)
            {
                MoveingAnimChange = true;
                
                zombieData.agent.speed = CurrentSpeed;
                zombieData.animator.SetFloat("Speed", CurrentSpeed);
                SturnColl.enabled = true;
                Trail.SetActive(true);
                
            }
        }
       
    }


    public override void Exit()
    {
        base.Exit();
    }


}
