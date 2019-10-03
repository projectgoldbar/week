using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalSystem : MonoBehaviour
{
    public Transform potalOut;
    private WaitForSeconds second = new WaitForSeconds(2f);

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = potalOut.transform.position;
    }
}