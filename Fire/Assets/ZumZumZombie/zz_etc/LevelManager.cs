using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform[] roadPreset;
    public Vector3[] roadPoint;
    public Vector3 endPoint;

    private void Awake()
    {
        var a = GameObject.Find("Roads");
        for (int i = 0; i < roadPoint.Length;)
        {
            var b = roadPreset[Random.Range(0, roadPreset.Length)];
            if (b.gameObject.activeSelf == false)
            {
                b.transform.position = roadPoint[i];
                b.gameObject.SetActive(true);
                i++;
            }
            continue;
        }
    }

    private void FixedUpdate()
    {
    }
}