using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private PlayerData playerData;

    private void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.transform.position);
    //    Debug.Log(playerData.transform.position);
    //    Debug.Log(other.transform.position - playerData.transform.position);
    //    other.transform.Translate((other.transform.position - playerData.transform.position) * 2f * Time.deltaTime);
    //}

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, playerData.transform.position, Time.deltaTime * 30);
    }
}