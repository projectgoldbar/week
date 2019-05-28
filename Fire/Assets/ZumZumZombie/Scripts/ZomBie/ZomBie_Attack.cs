using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    public class ZomBie_Attack : ZombieState
    {
        public override void Setting()
        {
            zombieData.zombieAttackCoroutine = Attack();
        }

        private Vector3 targetPoint;

        public override void Execute()
        {
            targetPoint = zombieData.player.position;
            StartCoroutine(zombieData.zombieAttackCoroutine);
        }

        private float ac = 60f;

        private IEnumerator Attack()

        {
            while (true)
            {
                //for (int i = 0; i < 45; i++)
                //{
                //    Debug.Log(i);
                //    transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * 3f);
                //    yield return null;
                //}
                zombieData.agent.ResetPath();
                yield return new WaitForSeconds(3f);
                // var a = zombieData.agent.velocity;

                //zombieData.agent.SetDestination(targetPoint);

                //zombieData.agent.speed = 60f;
                // transform.LookAt(targetPoint);
                // for (int i = 0; i < 60; i++)
                // {
                //     zombieData.agent.velocity = transform.forward * 30;
                // }
                //zombieData.agent.acceleration = ac;

                //ac = 60f;
                //zombieData.agent.velocity = a;

                StateChange(zombieData.moving);
                yield return null;
            }
        }

        public override void Exit()
        {
            //zombieData.agent.speed = 16f;
            StopCoroutine(zombieData.zombieAttackCoroutine);
        }
    }
}