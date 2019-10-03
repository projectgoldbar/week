using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject clearPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            GameLevelManager.instance.OnLifeTimeText();
            clearPanel.SetActive(true);
        }
    }
}