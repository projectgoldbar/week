using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("집에들어옴");
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameEnd();
        }
    }
}