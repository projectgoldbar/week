using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    public class SpitZombieMove : ZombieState
    {
        protected float resetPathCount = 0.2f;
        protected WaitForSeconds waitSecond;
        private Coroutine test;
        public Transform player;

        private Vector3 targetPosition = Vector3.zero;

        public Sector sector;

        public override void Setting()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
            zombieData.agent.acceleration = Random.Range(10, 18);
            //zombieData.moveCoroutine = ZombieMove();
            player = zombieData.player.gameObject.transform;

            sector = FindObjectOfType<Sector>();
            targetPosition = sector.FindPoint();

            zombieData.agent.SetDestination(targetPosition);
            zombieData.agent.SetPath(zombieData.path);

        }


     
        public override void Think()
        {
        }

        public override void Execute()
        {
           
        }

        private void Update()
        {
            
        }

    }
}
