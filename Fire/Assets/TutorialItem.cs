﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GameObject.FindObjectOfType<TutorialManager>().RootiedItem();
            Destroy(this.gameObject);
        }
    }
}