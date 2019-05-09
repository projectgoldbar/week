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
            zombieData.animator.Play("Zombie_Walk");
            StartCoroutine(zombieData.moveCoroutine);
        }

        public IEnumerator ZombieMove()
        {
            while (true)
            {
                //Debug.Log("calculate");
                zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                zombieData.agent.SetPath(zombieData.path);
                yield return waitSecond;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                StopCoroutine(zombieData.moveCoroutine);
            }
        }

        public override void Exit()
        {
            StopCoroutine(zombieData.moveCoroutine);
            this.enabled = false;
        }
    }
}