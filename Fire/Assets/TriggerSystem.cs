using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    public GameObject triggerObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GameObject.FindObjectOfType<AudioManager>().LV1AudioPlay();
            triggerObject.gameObject.SetActive(false);
        }
    }
}