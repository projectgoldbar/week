using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform target;

    public float distance;
    public int angle;

    private void Start()
    {
        //StartCoroutine(Checking(distance, angle));
    }
}
