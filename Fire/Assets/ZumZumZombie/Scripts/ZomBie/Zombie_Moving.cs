using UnityEngine;
using System.Collections;

namespace ZombieState
{
    public class Zombie_Moving : ZombieState
    {
        public CameraView ZombieCameraView;
        protected float resetPathCount = 0.2f;
        protected WaitForSeconds waitSecond;
        private Coroutine test;
        public Transform player;
        public Transform target = null;
        public float speed = 13f;
        public float attackCooltime = 10f;

        public override void Setting()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
            zombieData.agent.acceleration = Random.Range(10, 18);
            zombieData.moveCoroutine = ZombieMove();
            player = zombieData.player.gameObject.transform;
            target = player;
            speed = zombieData.player.GetComponent<PlayerMove>().maxSpeed + 5f;
        }

        public override void Execute()
        {
            zombieData.animator.SetBool("Attack", false);
            zombieData.agent.enabled = true;
            zombieData.agent.speed = speed;

            StartCoroutine(zombieData.moveCoroutine);
        }


        

        private bool x = true;

        public IEnumerator ZombieMove()
        {
            while (x)
            {
                yield return null;
                //zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                var x = zombieData.agent.isOnNavMesh;
                if (x)
                {
                    zombieData.agent.CalculatePath(target.position, zombieData.path);
                    zombieData.agent.SetPath(zombieData.path);
                }

                //Debug.Log("경로탐색중");
                yield return waitSecond;
            }
        }

        public float currentTime = 0;
        public float range = 20f;

        //박치기 좀비 돌릴라면 이거 주석해제

        //public override void Update()
        //{
        //    AttackChangeTime();
        //    if (Input.GetKeyDown(KeyCode.H))
        //    {
        //        MoveStop();
        //    }
        //}

        public virtual void AttackChangeTime()
        {
            var l = Vector3.Distance(transform.position, player.position);

            currentTime += Time.deltaTime;

            if (currentTime < attackCooltime)
            {
                return;
            }

            if (l < range)
            {
                currentTime = 0;
                StateChange(zombieData.attack);
            }
        }



        public override void Exit()
        {
            //StopCoroutine(zombieData.moveCoroutine);
            //zombieData.agent.ResetPath();

            zombieData.agent.speed = speed;

            //this.enabled = false;
        }
    }
}