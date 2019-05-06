using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    public class ZombieState : MonoBehaviour
    {
        protected ZombiesComponent zombieData;

        private void Awake()
        {
            zombieData = GetComponent<ZombiesComponent>();
        }

        public virtual void Initiate()
        {
        }

        public virtual void Think()
        {
        }

        public virtual void StateChange()
        {
        }

        public virtual void Execute()
        {
        }

        public virtual void Exit()
        {
        }
    }
}