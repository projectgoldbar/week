using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieState
{
    [DefaultExecutionOrder(-300)]
    public class ZombieState : MonoBehaviour
    {
        protected ZombiesComponent zombieData;

        private void Awake()
        {
            zombieData = GetComponent<ZombiesComponent>();
        }

        public virtual void Update()
        {
        }

        public virtual void Setting()
        {
        }

        public virtual void Initiate()
        {
        }

        public virtual void Think()
        {
        }

        public virtual void StateChange(ZombieState state)
        {
            zombieData.stateMachine.StateChange(state);
        }

        public virtual void Execute()
        {
        }

        public virtual void Exit()
        {
        }
    }
}