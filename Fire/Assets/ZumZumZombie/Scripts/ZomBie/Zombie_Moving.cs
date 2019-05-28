using UnityEngine;
using System.Collections;

namespace ZombieState
{
    public class Zombie_Moving : ZombieState
    {
        protected float resetPathCount = 0.2f;
        protected WaitForSeconds waitSecond;
        private Coroutine test;

        public override void Setting()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
            zombieData.agent.acceleration = Random.Range(10, 23);
            zombieData.moveCoroutine = ZombieMove();
        }

        public override void Think()
        {
        }

        public override void Execute()
        {
            //zombieData.agent.enabled = true;
            zombieData.animator.Play("Zombie_Walk");
            StartCoroutine(zombieData.moveCoroutine);
        }

        public IEnumerator ZombieMove()
        {
            while (true)
            {
                //Debug.Log("calculate");
                //if (Vector3.Distance(zombieData.player.position, transform.position) < 10f)
                //{
                //    //CoolTime(3f);
                //    //zombieData.agent.enabled = false;
                //    StateChange(zombieData.attack);
                //    yield return null;
                //}

                //zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                zombieData.agent.destination = zombieData.player.position;
                //zombieData.agent.SetPath(zombieData.path);

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
            if (Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(zombieData.moveCoroutine);
            }
        }

        public override void Exit()
        {
            StopCoroutine(zombieData.moveCoroutine);
            //this.enabled = false;
        }
    }
}