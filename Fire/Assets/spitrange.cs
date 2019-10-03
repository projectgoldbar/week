using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitrange : MonoBehaviour
{
    public SpitZombieMove SpitZombie;
    public BoxCollider coll;



    
    private void OnTriggerEnter(Collider other)
    {
        SpitZombie.SpitFire();
    }

}
