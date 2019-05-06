using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    [DefaultExecutionOrder(-300)]
    public class Zombie_Chase : Zombie_Moving
    {
        public float pathCount;

        public override void Initiate()
        {
            resetPathCount = pathCount;
            base.Initiate();
        }

        public override void Execute()
        {
            base.Execute();
            zombieData.animator.Play("Zombie_Walk");
        }
    }
}