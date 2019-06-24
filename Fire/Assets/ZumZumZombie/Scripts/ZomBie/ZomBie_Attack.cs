using UnityEngine;

namespace ZombieState
{
    public class ZomBie_Attack : ZombieState
    {
        public bool attackWait = true;

        public override void Setting()
        {
        }

        private Vector3 targetPoint;

        public override void Execute()
        {
            Debug.Log("공격상태로옴");

            attackWait = false;
            targetPoint = zombieData.player.position;
            zombieData.agent.acceleration = 50f;

            zombieData.attackTrailRenderer.enabled = true;
            zombieData.animator.SetBool("Attack", true);
            zombieData.animator.StopPlayback();
        }

        public float attackDelay = 0f;
        public float time = 0;
        public float attackRange = 30f;
        private Vector3 targetVec;

        public override void Update()
        {
            Debug.Log("공격업데이트");
            if (time > attackDelay)
            {
                Debug.DrawRay(transform.position, transform.forward.normalized * attackRange, Color.red, 5f);
                RaycastHit hit;
                attackWait = true;
                time = 0;
                if (Physics.Raycast(transform.position, transform.forward.normalized, out hit, attackRange, 1 << 11))
                {
                    Debug.Log("레이맞음");
                    targetVec = hit.point;
                    Debug.Log("targetPoint = " + targetPoint);
                }
                else
                {
                    targetVec = transform.position + transform.forward.normalized * attackRange;
                }
            }
            if (!attackWait)
            {
                //zombieData.animator.StartPlayback();
                time += Time.deltaTime;
                Vector3 a = (zombieData.player.position - transform.position);
                a.y = 1f;
                Quaternion rot = Quaternion.LookRotation((zombieData.player.position - transform.position));
                transform.rotation = rot;
                StopCoroutine(zombieData.moveCoroutine);
            }
            else
            {
                //Vector3 d = transform.position - targetVec;
                //d.Normalize();
                //Debug.Log(d);
                //transform.Translate(d + d * Time.deltaTime);
                Debug.Log(targetVec);
                transform.position = Vector3.Lerp(transform.position, targetVec, 2f * Time.deltaTime);
            }
        }

        //transform.Translate(transform.forward * -30f * Time.deltaTime);

        public void ChangeState()
        {
            Debug.Log("호출");
            zombieData.animator.StopPlayback();
            StateChange(zombieData.moving);
        }

        public override void Exit()
        {
            zombieData.attackTrailRenderer.enabled = false;

            zombieData.animator.SetBool("Attack", false);
            zombieData.agent.enabled = true;
            zombieData.agent.acceleration = 12f;
            //zombieData.agent.speed = 16f;
            //StopCoroutine(zombieData.zombieAttackCoroutine);
        }
    }
}