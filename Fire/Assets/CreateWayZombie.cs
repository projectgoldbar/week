using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWayZombie : MonoBehaviour
{
    public GameObject WayZombie;
    public List<GameObject> WayZombieList = new List<GameObject>();

    public Zombies_Position position;
    public Z_WayPointControll controll;



    private void Start()
    {
        Setting();
    }

    public void Setting()
    {
        for (int i = 0; i < position.ZombieMaxCount; i++)
        {
            var obj = GameObject.Instantiate(WayZombie, position.DefaultDistance[i], transform.rotation);
            obj.SetActive(false);
            WayZombieList.Add(obj);
        }
    }

   



}
