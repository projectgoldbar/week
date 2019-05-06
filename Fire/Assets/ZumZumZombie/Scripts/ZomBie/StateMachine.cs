using UnityEngine;

namespace ZombieState
{
    public class StateMachine : MonoBehaviour
    {
        public ZombieState currentState;

        private void Awake()
        {
            StateChange(GetComponent<Zombie_Chase>());
        }

        private void StateChange(ZombieState state)
        {
            //currentState.Exit();
            currentState = state;
            currentState.Initiate();
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