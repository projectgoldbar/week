﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxZone : MonoBehaviour
{
    public Animator DoorAnim;



    public GameObject[] Doors;

    private void OnTriggerEnter(Collider other)
    {
        //DoorAnim.Play("DoorOpen");
        //LayerNameChange();
    }
    public void LayerNameChange()
    {
        Doors[0].layer = 11;
        Doors[1].layer = 11;
    }

}