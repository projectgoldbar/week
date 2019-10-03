using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastZombie : MonoBehaviour
{
    public GameObject[] zombies;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (var item in zombies)
            {
                item.gameObject.SetActive(true);
            }
            GetComponent<Collider>().enabled = false;
        }
    }


}
