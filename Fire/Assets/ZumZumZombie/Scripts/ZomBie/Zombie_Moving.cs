using UnityEngine;
using System.Collections;

namespace ZombieState
{
    public class Zombie_Moving : ZombieState
    {
        protected float resetPathCount = 0.2f;
        protected WaitForSeconds waitSecond;
        private Coroutine test;
        public Transform player;
        public Transform target = null;
        public float speed = 13f;
        public float attackCooltime = 10f;

        public override void Setting()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
            zombieData.agent.acceleration = Random.Range(10, 18);
            zombieData.moveCoroutine = ZombieMove();
            player = zombieData.player.gameObject.transform;
            target = player;
        }

        public override void Think()
        {
        }

        public override void Execute()
        {
            zombieData.animator.SetBool("Attack", false);
            zombieData.agent.speed = speed;

            //test = StartCoroutine(ZombieMove());
            //zombieData.agent.enabled = true;
            StartCoroutine(zombieData.moveCoroutine);
        }

        public void MoveStop()
        {
            //x = false;
            //if(zombieData.agent.path == null)
            //zombieData.agent.ResetPath();
            //StopCoroutine(zombieData.moveCoroutine);
            //StopCoroutine(test);
        }

        public void MoveStart()
        {
            //StartCoroutine(zombieData.moveCoroutine);
        }

        private bool x = true;

        public IEnumerator ZombieMove()
        {
            while (x)
            {
                yield return null;
                //zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                zombieData.agent.CalculatePath(target.position, zombieData.path);
                zombieData.agent.SetPath(zombieData.path);

                Debug.Log("경로탐색중");
                yield return waitSecond;
            }
        }

        public float currentTime = 0;
        public float range = 20f;

        public override void Update()
        {
            //Debug.Log("aaa");

            AttackChangeTime();

            //zombieData.animator.SetFloat("Distance", l);
            if (Input.GetKeyDown(KeyCode.H))
            {
                //StopCoroutine(test);
                //zombieData.agent.ResetPath();
                MoveStop();
            }
        }

        public virtual void AttackChangeTime()
        {
            var l = Vector3.Distance(transform.position, player.position);

            currentTime += Time.deltaTime;

            if (currentTime < attackCooltime)
            {
                return;
            }

            if (l < range)
            {
                currentTime = 0;
                StateChange(zombieData.attack);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        public override void Exit()
        {
            //StopCoroutine(zombieData.moveCoroutine);
            //zombieData.agent.ResetPath();
            zombieData.agent.speed = speed;

            //this.enabled = false;
        }
    }
}