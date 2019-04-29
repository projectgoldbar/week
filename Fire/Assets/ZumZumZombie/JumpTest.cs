using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    public Rigidbody rb;

    //private void Awake()
    //{
    //    rb.GetComponent<Rigidbody>();
    //}

    //private void OnEnable()
    //{
    //    rb.GetComponent<Rigidbody>();
    //}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("A");
            transform.Translate(transform.up * 1f * Time.deltaTime);
        }
    }
}