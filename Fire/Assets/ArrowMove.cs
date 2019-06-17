using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        Vector3 vec = target.position - transform.position;
        vec.y = transform.position.y;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
}