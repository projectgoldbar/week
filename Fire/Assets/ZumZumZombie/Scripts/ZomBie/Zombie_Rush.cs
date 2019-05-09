using System.Collections;
using UnityEngine;

namespace ZombieState
{
    public class Zombie_Rush : ZomBie_Attack
    {
        public float chargeTime = 2f;
        public float attackDelay = 1f;
        public float range = 30f;
        private WaitForSeconds delay;
        private Vector3 pivot;

        public override void Setting()
        {
            zombieData.zombieAttackCoroutine = Attack();
            delay = new WaitForSeconds(attackDelay);
        }

        public override void Execute()
        {
            AttackProcess();
        }

        private void AttackProcess()
        {
            //도착지점 설정
            var v = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
            zombieData.eyeTrailRenderer.time = 0.5f;
            //zombieData.agent.speed = 0;
            RaycastHit hit;

            if (Physics.Raycast(v, transform.forward, out hit, range, 1 << 11))
            {
                pivot = Vector3.Lerp(transform.position, hit.point, 0.8f);
                pivot.y -= 5f;
            }
            else
            {
                pivot = transform.position + transform.forward * range;
            }
            //이동

            StartCoroutine(zombieData.zombieAttackCoroutine);
        }

        public ParticleSystem ps;

        private IEnumerator Attack()
        {
            while (true)
            {
                zombieData.material.color = Color.red;
                var b = Vector3.Distance(pivot, transform.position);
                var c = (pivot + transform.position) / 2;
                EnemyAttackUIManager.instance.Draw(1f, c, pivot, this.transform.position, b);

                yield return delay;
                zombieData.material.color = Color.white;

                for (int i = 0; i < 23; i++)
                {
                    transform.position = Vector3.Lerp(transform.position, pivot, Time.deltaTime * 3f);
                    yield return null;
                }

                StateChange(zombieData.moving);
                yield return null;
            }
        }

        public override void Exit()
        {
            zombieData.eyeTrailRenderer.time = 0.0f;
            //zombieData.agent.speed = 16;
            StopCoroutine(zombieData.zombieAttackCoroutine);
            this.enabled = false;
        }
    }
}