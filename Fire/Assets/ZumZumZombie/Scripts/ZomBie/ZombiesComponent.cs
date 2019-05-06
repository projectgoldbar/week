using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZombieState
{
    [DefaultExecutionOrder(-100)]
    public class ZombiesComponent : MonoBehaviour
    {
        public NavMeshAgent agent;
        public NavMeshPath path;
        public Transform player;
        public Animator animator;

        private void Awake()
        {
            path = new NavMeshPath();
            player = GameObject.FindObjectOfType<Player>().transform;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }
    }
}