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

        public override void Setting()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
            zombieData.agent.acceleration = Random.Range(10, 23);
            //zombieData.moveCoroutine = ZombieMove();
            player = zombieData.player.gameObject.transform;
        }

        public override void Think()
        {
        }

        public override void Execute()
        {
            test = StartCoroutine(ZombieMove());
            //zombieData.agent.enabled = true;
            //StartCoroutine(zombieData.moveCoroutine);
        }

        public IEnumerator ZombieMove()
        {
            while (true)
            {
                yield return null;
                zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                zombieData.agent.SetPath(zombieData.path);
                yield return waitSecond;
            }
        }

        public void CoolTime(float timer)
        {
            float currentTime = 0;

            for (; currentTime <= timer;)
            {
                currentTime += 1 * Time.deltaTime;
                Debug.Log(currentTime);
            }
        }

        private void Update()
        {
            var l = Vector3.Distance(transform.position, player.position);

            zombieData.animator.SetFloat("Distance", l);
            if (Input.GetKeyDown(KeyCode.H))
            {
                StopCoroutine(test);
                zombieData.agent.ResetPath();
            }
        }

        public override void Exit()
        {
            //StopCoroutine(zombieData.moveCoroutine);
            //this.enabled = false;
        }
    }
}