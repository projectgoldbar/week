using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    public class SpeedZomMoving : Zombie_Moving
    {

        private float DefaultSpeed = 13;

        [Header("증가 스피드")]
        public float AddSpeed = 0.1f;
        [Header("현재 스피드")]
        public float CurrentSpeed = 13f;
        [Header("최대 스피드")]
        public float MaxSpeed = 50f;

        private float RunnerTimer = 0;
        private float RunnerMaxTimer = 5.0f;

        public bool MoveingAnimChange = false;

        public Collider SturnColl;
        public GameObject Trail;

        private void OnEnable()
        {
            SturnCollider.SturnEvent += SturnChange;
            Trail.SetActive(false);
        }

        public override void Execute()
        {
            Debug.Log("Move");
            zombieData.agent.velocity = Vector3.forward;
            CurrentSpeed = DefaultSpeed;
            zombieData.agent.speed = CurrentSpeed;
            SturnColl.enabled = false;
            StartCoroutine(zombieData.moveCoroutine);
        }

        public override void Update()
        {


            //1. 일정한 시간이 지나면 속도를 증가시킴 
            //2. MAx스피드까지 오르면 애니메이션 변경?
            //3. 변경 후 건물에 부딛힐경우 넘어지는 애니매이션 실행 (스턴상태)

            RunnerTimer += Time.deltaTime;

            if (RunnerTimer >= RunnerMaxTimer && MaxSpeed > CurrentSpeed)
            {
                RunnerTimer = 0;
                CurrentSpeed += AddSpeed;
                zombieData.agent.speed = CurrentSpeed;

            }

            if (MaxSpeed <= CurrentSpeed)
            {
                MoveingAnimChange = true;

                {
                    //2.애니메이션 변경 
                    //2.1 상태 전환?
                    //2.2 그대로 애니매이션만 변경?
                    zombieData.animator.SetFloat("Speed", CurrentSpeed);
                    SturnColl.enabled = true;
                    Trail.SetActive(true);
                }
            }


        }

        public override void AttackChangeTime()
        {
            
        }




        public void SturnChange()
        {
            if (MoveingAnimChange)
                StateChange(zombieData.stun);
        }


        public override void Exit()
        {
            Trail.SetActive(false);
            MoveingAnimChange = false;
            CurrentSpeed = 0;
            zombieData.agent.speed = CurrentSpeed;
            zombieData.agent.velocity = Vector3.zero;
            zombieData.animator.SetFloat("Speed", CurrentSpeed);
        }


      
    }
}
