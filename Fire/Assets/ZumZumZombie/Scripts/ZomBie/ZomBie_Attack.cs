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
            speed = originSpeed + zombieData.player.GetComponent<PlayerMove>().maxSpeed;
            zombieData.animator.SetLayerWeight(1, 0);
            attackWait = false;
            targetPoint = zombieData.player.position;
            zombieData.agent.acceleration = 50f;

            eyeParticle.Play();
            zombieData.animator.SetBool("Attack", true);
        }

        public float originSpeed;
        public float speed;
        public float attackDelay = 0f;
        public float time = 0;
        public float attackRange = 30f;
        private Vector3 targetVec;

        public override void Update()
        {
            if (time > attackDelay)
            {
                attackWait = true;
                time = 0;
                }
            if (!attackWait)
            {
                time += Time.deltaTime;
                Vector3 a = (zombieData.player.position - transform.position);
                a.y = 1f;
                Quaternion rot = Quaternion.LookRotation((zombieData.player.position - transform.position));
                transform.rotation = rot;
                zombieData.sturnCollider.gameObject.SetActive(true);
            }
            else
            {
                zombieData.attackTrailRenderer.enabled = true;
                zombieData.animator.StopPlayback();
                zombieData.animator.Play("AttackCrow");
                zombieData.agent.acceleration = 0f;
                zombieData.agent.enabled = false;

                time += Time.deltaTime;

                transform.position = transform.position + transform.forward.normalized * speed * Time.deltaTime;
            }
        }


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
            zombieData.agent.acceleration = 11f;
            zombieData.animator.SetBool("Attack", false);

        }
    }
}