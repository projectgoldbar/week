using UnityEngine;
using System.Collections;

namespace ZombieState
{
    public class Zombie_Runaway : ZombieState
    {
        protected float resetPathCount = 0.2f;
        protected WaitForSeconds waitSecond;
        private Coroutine test;
        public Transform player;

        public Transform target = null;

        public override void Setting()
        {
            waitSecond = new WaitForSeconds(resetPathCount);
            zombieData.agent.acceleration = Random.Range(10, 18);
            //zombieData.moveCoroutine = ZombieMove();
            player = zombieData.player.gameObject.transform;
            target = player;
        }

        public override void Think()
        {
        }

        public override void Execute()
        {
            test = StartCoroutine(RunawayZombieAi());
        }

        private bool x = true;

        private void Update()
        {
        }

        public IEnumerator RunawayZombieAi()
        {
            while (x)
            {
                var distance = Vector3.Distance(zombieData.player.position, this.transform.position);
                if (distance > 10f)
                {
                    //zombieData.agent.CalculatePath(zombieData.player.position, zombieData.path);
                    zombieData.agent.CalculatePath(target.position, zombieData.path);
                    zombieData.agent.SetPath(zombieData.path);
                }
                else
                {
                    FindFarPoint(transform.position);
                }
                yield return waitSecond;
            }

            if (zombieData.agent.path == null)
                zombieData.agent.ResetPath();
            yield return null;
        }

        public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 6f, float maxDistance = 10f)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(target.rotation.eulerAngles.y * -1f, -target.rotation.eulerAngles.y + 90);
            float radian = (angle * Mathf.Deg2Rad) + 45f;
            return pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
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

        public override void Exit()
        {
            //StopCoroutine(zombieData.moveCoroutine);
            //this.enabled = false;
        }
    }
}