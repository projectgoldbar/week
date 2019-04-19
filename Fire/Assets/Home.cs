using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("a");
        if (collision.transform.tag == "Player")
        {
            GameManager.instance.GameEnd();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");
        if (other.transform.tag == "Player")
        {
            GameManager.instance.GameEnd();
        }
    }
}