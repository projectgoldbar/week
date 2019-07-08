using UnityEngine;

namespace ZombieState
{
    public class Zombie_AttackRun : ZomBie_Attack
    {
        public override void Setting()
        {
            stunSpeed = zombieData.player.GetComponent<PlayerMove>().speed + 3f;
            zombieData.sturnCollider.SturnEvent += SturnChange;
        }

        public override void Execute()
        {
            //전환효과
            for (int i = 0; i < zombieData.attackTrail.Length; i++)
            {
                zombieData.attackTrail[i].enabled = true;
            }
            //zombieData.animator.SetLayerWeight(1, 1);
        }

        public float accelDelay = 1f;
        public float accelSpeed = 0.5f;
        public float maxSpeed = 30f;
        public float stunSpeed = 20f;
        public int count = 0;

        public override void Update()
        {
            time += Time.deltaTime;
            if (time > accelDelay)
            {
                if (maxSpeed < zombieData.agent.speed)
                {
                    return;
                }
                else if (count == 2)
                {
                    zombieData.sturnCollider.gameObject.SetActive(true);
                    zombieData.animator.StopPlayback();
                    zombieData.animator.SetBool("FastRun", true);
                    count++;
                }
                else
                {
                    zombieData.agent.speed += accelSpeed;
                    count++;
                }
                time = 0f;
            }
        }

        public (float dis, Vector3 dir) DisNdir(Vector3 aa, Vector3 bb)
        {
            var Init = (aa - bb);

            var dir = Init.normalized;
            var dis = Init.magnitude;

            return (dis, dir);
        }

        public override void Exit()
        {
            count = 0;
            zombieData.animator.StopPlayback();
            zombieData.animator.SetBool("FastRun", false);
        }
    }
}