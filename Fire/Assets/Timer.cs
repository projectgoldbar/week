using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time;

    private void Update()
    {
        time += Time.deltaTime;
        Debug.Log(Mathf.Ceil(time));
    }
}