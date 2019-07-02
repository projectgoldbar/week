using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieState;

public class Zombie1Moving : SpeedZomMoving
{

    public CameraView ZombieCameraView;


    [Header("플레이어 속도")]
    public float PlayerSpeed = 10;


    private float RunnerTimer = 0;
    private float RunnerMaxTimer = 5.0f;

    public override void Execute()
    {
        base.Execute();

        PlayerSpeed = player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
       
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
                {
                    zombieData.animator.SetFloat("Speed", CurrentSpeed);
                    SturnColl.enabled = true;
                    Trail.SetActive(true);
                }
            }
        }
        else
        {

            zombieData.agent.speed = 20;
        }
    }


    public override void Exit()
    {
        base.Exit();
    }


}
