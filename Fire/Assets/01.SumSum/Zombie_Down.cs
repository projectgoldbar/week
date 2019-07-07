using UnityEngine;

namespace ZombieState
{
    public class Zombie_Down : ZombieState
    {
        public override void Setting()
        {
        }

        public override void Execute()
        {
            transform.tag = "Zombie";
            //zombieData.animator.speed = 0f;
            //다운애니메이션출력
        }

        private float currentTime = 0f;
        public float reviveTime = 5f;

        public override void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime > reviveTime)
            {
                currentTime = 0f;
                zombieData.animator.speed = 1f;
            }
        }

        public void SetRecoverTime()
        {
            zombieData.animator.speed = 0f;
        }

        public void GoSlowMove()
        {
            StateChange(zombieData.slowMoving);
        }

        public override void Exit()
        {
            zombieData.agent.enabled = true;
        }
    }
}