using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisonzone : MonoBehaviour
{
    private BoxCollider Zone;
    private ParticleSystem Poision;


    void Awake()
    {
        Zone = GetComponent<BoxCollider>();
        Poision = GetComponentInChildren<ParticleSystem>();
    }



    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }


    // Update is called once per frame
}
