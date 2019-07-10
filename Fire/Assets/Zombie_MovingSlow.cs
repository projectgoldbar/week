using UnityEngine;

namespace ZombieState
{
    public class Zombie_MovingSlow : ZombieState
    {
        public float slowSpeed = 0f;
        private float currentTime = 0f;

        [Header("가속하기까지대기시간")]
        public float attackCoolTime = 300f;

        public override void Setting()
        {
            slowSpeed = zombieData.player.GetComponent<PlayerMove>().maxSpeed;
        }

        public override void Execute()
        {
            zombieData.agent.enabled = true;
            zombieData.animator.SetBool("FastRun", false);
            zombieData.agent.speed = slowSpeed;
        }

        public override void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackCoolTime)
            {
                currentTime = 0f;
                StateChange(zombieData.attack);
            }
        }

        public override void Exit()
        {
        }
    }
}