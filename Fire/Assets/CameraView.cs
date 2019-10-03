using UnityEngine;
using ZombieState;

public class CameraView : MonoBehaviour
{
    public ZombiesComponent zombiedata;

    private void OnBecameVisible()
    {
        if (zombiedata.stateMachine.currentState == zombiedata.moving)
        {
            zombiedata.stateMachine.StateChange(zombiedata.slowMoving);
        }
    }

    private void OnBecameInvisible()
    {
        if (zombiedata.stateMachine.currentState == zombiedata.moving)
        {
            zombiedata.agent.speed = 20f;
        }
        else if (zombiedata.stateMachine.currentState == zombiedata.slowMoving)
        {
            zombiedata.stateMachine.StateChange(zombiedata.moving);
        }

        //zombiedata.stateMachine.StateChange(zombiedata.moving);
        //zombiedata.agent.speed = 20f;
        //MoveSpeedChange = false;
    }
}