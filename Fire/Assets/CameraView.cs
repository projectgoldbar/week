using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieState;

public class CameraView : MonoBehaviour
{
    public ZombiesComponent zombiedata;
    public Zombie1Moving zombie;
    public bool MoveSpeedChange = false;

    private void OnBecameVisible()
    {
        zombiedata.stateMachine.StateChange(zombiedata.slowMoving);
        //if (zombie.Running == false)
        //{
        //    zombie.CurrentSpeed = zombie.PlayerSpeed;
        //    zombiedata.agent.speed = zombie.CurrentSpeed;
        //}
        //MoveSpeedChange = true;
    }

    private void OnBecameInvisible()
    {
        zombiedata.animator.speed = 1f;

        //zombiedata.stateMachine.StateChange(zombiedata.moving);
        //zombiedata.agent.speed = 20f;
        //MoveSpeedChange = false;
    }
}