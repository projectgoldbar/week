using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zako : MonoBehaviour
{
    private Transform target;
    public float speed = 3f;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        transform.Translate(transform.forward * -10f * Time.deltaTime);
    }
}