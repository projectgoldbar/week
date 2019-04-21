using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameEnd();
        }
    }
}