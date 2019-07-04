using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace ZombieState
{
    [DefaultExecutionOrder(-1000)]
    public class ZombiesComponent : MonoBehaviour
    {
        public int level = 0;
        public int damage = 1;
        public NavMeshAgent agent;
        public NavMeshPath path;
        public Transform player;
        public Animator animator;
        public StateMachine stateMachine;
        public Zombie_Moving moving;
        public ZomBie_Attack attack;
        public ZomBie_Stun stun;
        public Zombie_MovingSlow slowMoving;
        public SturnCollider sturnCollider;
        public IEnumerator moveCoroutine;
        public IEnumerator canIAttackCoroutine;
        public IEnumerator zombieAttackCoroutine;
        public Material material;
        public TrailRenderer attackTrailRenderer;
        public ParticlePool particlePool;

        private void Awake()
        {
            path = new NavMeshPath();
            player = GameObject.FindObjectOfType<PlayerMove>().transform;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            stateMachine = GetComponent<StateMachine>();
            moving = GetComponent<Zombie_Moving>();
            slowMoving = GetComponent<Zombie_MovingSlow>();
            moving.enabled = false;
            attack = GetComponent<ZomBie_Attack>();
            particlePool = FindObjectOfType<ParticlePool>();
            stun = GetComponent<ZomBie_Stun>();
            stun.enabled = false;
            material = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
            if (attack != null)
                attack.enabled = false;
        }
    }
}