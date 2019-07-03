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
        if (zombie.Running == false)
        {
            zombie.CurrentSpeed = zombie.PlayerSpeed;
            zombiedata.agent.speed = zombie.CurrentSpeed;
        }
         MoveSpeedChange = true;
    }
    private void OnBecameInvisible()
    {
        MoveSpeedChange = false;
    }

}
