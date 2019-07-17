using UnityEngine;

namespace ZombieState
{
    public class ZomBie_Stun : ZombieState
    {
        public float stun = 1f;
        public float wallStun = 2f;
        public float time = 0f;
        public bool isWall = false;

        public override void Setting()
        {
        }

        public override void Execute()
        {
            if (isWall)
            {
                zombieData.animator.SetBool("IsWall", true);
                zombieData.agent.velocity = new Vector3(0f, 0f, 0f);
                zombieData.agent.speed = 0;
                zombieData.agent.acceleration = 0f;
            }
            else
            {
                zombieData.animator.SetBool("IsWall", false);
                StateChange(zombieData.moving);
            }
        }

        public void ChangeStat()
        {
            StateChange(zombieData.moving);
        }

        public override void Exit()
        {
            zombieData.agent.enabled = true;
            //zombieData.animator.speed = 1;
            zombieData.agent.speed = 13f;

            zombieData.agent.acceleration = 12f;
        }
    }
}