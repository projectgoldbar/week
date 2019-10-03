using UnityEngine;

namespace ZombieState
{
    public class StateMachine : MonoBehaviour
    {
        public ZombieState currentState;

        private void Awake()
        {
            AllStateSetup();
        }

        private void OnEnable()
        {
            if (currentState == null)
            {
                currentState = GetComponent<Zombie_Moving>();
                currentState.enabled = true;
            }
            StateStart();
        }

        public void AllStateSetup()
        {
            var a = GetComponents<ZombieState>();
            for (int i = 0; i < a.Length; i++)
            {
                a[i].Setting();
            }
        }

        public void StateChange(ZombieState state)
        {
            if (currentState != state)
            {
                currentState.Exit();
                currentState.enabled = false;
                currentState = state;
                currentState.enabled = true;

                StateStart();
            }
        }

        public void StateStart()
        {
            currentState.Execute();
        }


    }
}