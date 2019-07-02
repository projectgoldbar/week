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
        zombiedata.agent.speed = zombie.PlayerSpeed;
        MoveSpeedChange = true;
    }

    //화면 밖으로 나가면 호출된다. (Scene/Game)둘다 적용됨.
    private void OnBecameInvisible()
    {
        MoveSpeedChange = false;
    }
}
