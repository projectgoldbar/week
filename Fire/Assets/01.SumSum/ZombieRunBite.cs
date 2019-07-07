using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    public class ZombieRunBite : Zombie_AttackRun
    {
        public override void Execute()
        {
            transform.tag = "BiteZombie";
        }

        public void ChangeBite()
        {
            StateChange(zombieData.zombieBite);
        }
    }
}