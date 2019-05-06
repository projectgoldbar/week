using UnityEngine;
using System.Collections;

namespace ZombieState
{
    public class Zombie_Moving : ZombieState
    {
        protected float resetPathCount = 2;
        protected WaitForSeconds waitSecond;

        public override void Initiate()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
        }

        public override void Think()
        {
        }

        public override void Execute()
        {
            StartCoroutine(ZombieMove());
        }

        public IEnumerator ZombieMove()
        {
            while (true)
            {
                zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                zombieData.agent.SetPath(zombieData.path);
                yield return waitSecond;
            }
        }

        public override void StateChange()
        {
        }

        public override void Exit()
        {
            StopCoroutine(this.ZombieMove());
        }
    }
}