using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    public class Zombie_Bite : ZombieState
    {
        public bool isbite = false;

        public override void Setting()
        {
        }

        public override void Execute()
        {
            zombieData.agent.enabled = false;
        }

        public void ZombieDown()
        {
            StateChange(zombieData.zombieDown);
        }

        public override void Exit()
        {
        }
    }
}