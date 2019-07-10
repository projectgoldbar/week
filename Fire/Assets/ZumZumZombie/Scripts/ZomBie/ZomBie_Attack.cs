using UnityEngine;

namespace ZombieState
{
    public class ZomBie_Attack : ZombieState
    {
        public bool attackWait = true;
        public bool iswall = false;
        public ParticleSystem eyeParticle;

        public override void Setting()
        {
            zombieData.sturnCollider.SturnEvent += SturnChange;
        }

        private Vector3 targetPoint;

        public override void Execute()
        {
            zombieData.animator.SetLayerWeight(1, 0);
            Debug.Log("공격상태로옴");
            attackWait = false;
            targetPoint = zombieData.player.position;
            zombieData.agent.acceleration = 50f;
            eyeParticle.Play();
            zombieData.attackTrailRenderer.enabled = true;
            zombieData.animator.SetBool("Attack", true);
        }

        public float attackDelay = 0f;
        public float time = 0;
        public float attackRange = 30f;
        private Vector3 targetVec;

        public override void Update()
        {
            if (time > attackDelay)
            {
                Debug.DrawRay(transform.position, transform.forward.normalized * attackRange, Color.red, 5f);
                attackWait = true;
                time = 0;
                //if (Physics.Raycast(transform.position, transform.forward.normalized, out hit, attackRange, 1 << 11))
                //{
                //    Debug.Log("레이맞음");
                //    targetVec = hit.point;
                //    iswall = true;
                //    Debug.Log("targetPoint = " + targetPoint);
                //}
                //else
                //{
                //    targetVec = transform.position + transform.forward.normalized * attackRange;
                //    iswall = false;
                //}
                //zombieData.animator.Play("mixamo_com(1)");
            }
            if (!attackWait)
            {
                //zombieData.animator.StartPlayback();
                time += Time.deltaTime;
                Vector3 a = (zombieData.player.position - transform.position);
                a.y = 1f;
                Quaternion rot = Quaternion.LookRotation((zombieData.player.position - transform.position));
                transform.rotation = rot;
                zombieData.sturnCollider.gameObject.SetActive(true);
                //StopCoroutine(zombieData.moveCoroutine);
            }
            else
            {
                //Vector3 d = transform.position - targetVec;
                //d.Normalize();
                //Debug.Log(d);
                //transform.Translate(d + d * Time.deltaTime);
                Debug.Log("돌진한다");
                zombieData.animator.StopPlayback();
                zombieData.animator.Play("AttackCrow");
                zombieData.agent.acceleration = 0f;
                zombieData.agent.enabled = false;
                //zombieData.agent.velocity = zombieData.agent.transform.forward * 20f;

                time += Time.deltaTime;

                //transform.position = Vector3.Lerp(transform.position, targetVec, 2f * Time.deltaTime);
                transform.position = transform.position + transform.forward.normalized * 30f * Time.deltaTime;
            }
        }

        //transform.Translate(transform.forward * -30f * Time.deltaTime);

        public virtual void SturnChange()
        {
            StateChange(zombieData.stun);
        }

        public void ChangeState()
        {
            StateChange(zombieData.slowMoving);
        }

        public override void Exit()
        {
            time = 0f;
            zombieData.sturnCollider.gameObject.SetActive(false);
            zombieData.attackTrailRenderer.enabled = false;
            //zombieData.agent.enabled = true;
            zombieData.agent.acceleration = 11f;
            zombieData.animator.SetBool("Attack", false);

            //zombieData.animator.speed = 1;
            //zombieData.agent.acceleration = 0f;
            //zombieData.agent.velocity = new Vector3(0, 0, 0);
            //zombieData.agent.speed = 0f;
            //zombieData.animator.SetBool("Attack", false);
            //zombieData.attackTrailRenderer.enabled = false;
            //time = 0f;
            //zombieData.agent.speed = 16f;
            //StopCoroutine(zombieData.zombieAttackCoroutine);
        }
    }
}