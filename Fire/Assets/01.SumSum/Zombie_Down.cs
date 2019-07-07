using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    public class Zombie_Down : ZombieState
    {
        public override void Setting()
        {
        }

        public override void Execute()
        {
            transform.tag = "Zombie";
        }

        public override void Exit()
        {
        }
    }
}