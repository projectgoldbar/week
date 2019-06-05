using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator doorAnim;


    void Start()
    {
        doorAnim = this.GetComponent<Animator>();
    }



   

    private void OnTriggerEnter(Collider other)
    {
        if (CheckPlayerIn(other))
        {
            CloseDoor();
        }
}



    bool CheckPlayerIn(Collider other)
    {
        if (other.tag == "Player")
        {
            return true;
        }
        return false;
    }

    void CloseDoor()
    {
        doorAnim.SetTrigger("PlayerIn");
    }
}
