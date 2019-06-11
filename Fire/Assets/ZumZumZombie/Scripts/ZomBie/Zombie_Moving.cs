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

        public override void Setting()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
            zombieData.agent.acceleration = Random.Range(10, 18);
            //zombieData.moveCoroutine = ZombieMove();
            player = zombieData.player.gameObject.transform;
            target = player;
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
        public void MoveStop()
        {
            x = false;
            if(zombieData.agent.path == null)
            zombieData.agent.ResetPath();
            //StopCoroutine(zombieData.moveCoroutine);
            StopCoroutine(test);


        }
        public void MoveStart()
        {
            x = true;

            //StartCoroutine(zombieData.moveCoroutine);
            test = StartCoroutine(ZombieMove());
        }

        bool x = true;
        public IEnumerator ZombieMove()
        {
            while (x)
            {
                //zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                zombieData.agent.CalculatePath(target.position, zombieData.path);
                zombieData.agent.SetPath(zombieData.path);
                yield return waitSecond;
            }
            if (zombieData.agent.path == null)
                zombieData.agent.ResetPath();
            yield return null;
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
                //StopCoroutine(test);
                //zombieData.agent.ResetPath();
                MoveStop();
            }
        }

        public override void Exit()
        {
            //StopCoroutine(zombieData.moveCoroutine);
            //this.enabled = false;
        }
    }
}