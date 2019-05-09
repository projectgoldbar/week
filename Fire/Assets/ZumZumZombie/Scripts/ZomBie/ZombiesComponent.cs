using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace ZombieState
{
    [DefaultExecutionOrder(-100)]
    public class ZombiesComponent : MonoBehaviour
    {
        public int level = 0;
        public NavMeshAgent agent;
        public NavMeshPath path;
        public Transform player;
        public Animator animator;
        public StateMachine stateMachine;
        public Zombie_Moving moving;
        public ZomBie_Attack attack;
        public ZombieState stun;
        public IEnumerator moveCoroutine;
        public IEnumerator canIAttackCoroutine;
        public IEnumerator zombieAttackCoroutine;
        public Material material;
        public TrailRenderer eyeTrailRenderer;

        private void Awake()
        {
            path = new NavMeshPath();
            player = GameObject.FindObjectOfType<Player>().transform;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            stateMachine = GetComponent<StateMachine>();
            moving = GetComponent<Zombie_Moving>();
            attack = GetComponent<ZomBie_Attack>();
            material = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                StopCoroutine(moveCoroutine);
            }
        }
    }
}