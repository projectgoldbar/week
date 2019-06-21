using UnityEngine;

namespace ZombieState
{
    public class StateMachine : MonoBehaviour
    {
        public ZombieState currentState;

        private void Awake()
        {
            if (currentState == null)
            {
                currentState = GetComponent<Zombie_Moving>();
                currentState.enabled = true;
            }
            AllStateSetup();
            StateStart();
        }

        public void AllStateSetup()
        {
            var a = FindObjectsOfType<ZombieState>();
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentState.Execute();
            }
        }
    }
}