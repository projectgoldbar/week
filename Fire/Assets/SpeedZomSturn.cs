﻿using UnityEngine;

namespace ZombieState
{
    public class SpeedZomSturn : ZomBie_Stun
    {
        public override void Execute()
        {
            //애니메이션 실행
            //정면에서 부딛힌 상황 뒤로 넘어지는 애니메이션
            zombieData.animator.SetLayerWeight(1, 0);
            //zombieData.agent.speed = 0f;
            //zombieData.agent.velocity = Vector3.zero;
            zombieData.agent.enabled = false;
            var x = zombieData.particlePool.GetParticle(zombieData.particlePool.hitParticlePool);

            x.transform.position = transform.position;
            x.transform.localRotation = transform.rotation;
            x.SetActive(true);
            zombieData.animator.SetFloat("Speed", zombieData.agent.speed);
            zombieData.animator.StopPlayback();
            zombieData.animator.Play("Blow");
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }

        public float recoverTime = 1f;

        public void EndSturn()
        {
            zombieData.sturnCollider.gameObject.SetActive(false);

            Debug.Log("스턴끝");
            zombieData.animator.SetBool("Sturn", false);

            StateChange(zombieData.zombieDown);
        }
    }
}