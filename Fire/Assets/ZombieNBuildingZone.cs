using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNBuildingZone : MonoBehaviour
{
    public Animator DoorAnim;



    private void OnTriggerEnter(Collider other)
    {
        DoorAnim.Play("DoorOpen");
    }
    public void LayerNameChange()
    {
        gameObject.layer = 11;
    }
}
