using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public Vector3 target = new Vector3(0, 0, 0);

    private void Update()
    {
        Vector3 dir = target - transform.position;
        dir.y = transform.position.y;
        dir.Normalize();
        Quaternion q = Quaternion.LookRotation(dir);
        transform.rotation = q;
    }
}