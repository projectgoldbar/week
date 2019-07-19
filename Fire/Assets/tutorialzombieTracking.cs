using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialzombieTracking : MonoBehaviour
{

    public static int ZombieSturnCounting;


    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<BoxCollider>().enabled == true)
        {
            ZombieSturnCounting++;
            Debug.Log(ZombieSturnCounting);

        }
        else
            return;
    }


}
