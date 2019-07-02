using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SturnCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public Action SturnEvent = () => { };

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("박음");

        SturnEvent?.Invoke();
    }
}