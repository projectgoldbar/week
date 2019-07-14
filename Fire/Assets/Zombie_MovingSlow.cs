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
        }

        public override void Execute()
        {
            slowSpeed = zombieData.player.GetComponent<PlayerMove>().maxSpeed;
            zombieData.agent.enabled = true;
            zombieData.animator.SetBool("FastRun", false);
            zombieData.agent.speed = slowSpeed;
            var distance = Vector3.Distance(transform.position, zombieData.player.position);
            if (distance > 50f)
            {
                StateChange(zombieData.moving);
            }

            var x = Random.Range(0, 5);
            if (x == 0)
            {
                SoundManager.Instance.PlaySoundSFX("ZOMBIEMALE0");
            }
            else if (x == 1)
            {
                SoundManager.Instance.PlaySoundSFX("ZOMBIEMALE1");
            }
            else if (x == 2)
            {
                SoundManager.Instance.PlaySoundSFX("ZOMBIEFEMALE0");
            }
            else if (x == 3)
            {
                SoundManager.Instance.PlaySoundSFX("ZOMBIEFEMALE1");
            }
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