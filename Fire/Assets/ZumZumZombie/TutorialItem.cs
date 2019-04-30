using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    public GameObject homePointer;
    public GameObject arrowPointer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            homePointer.SetActive(true);
            arrowPointer.SetActive(false);
            GameObject.FindObjectOfType<TutorialManager>().RootiedItem();
            GameObject.FindObjectOfType<AudioManager>().LV2AudioPlay();

            Destroy(this.gameObject);
        }
    }
}