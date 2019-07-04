using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightfallow : MonoBehaviour
{
    public Transform Target = null;

    public float dumpSpeed = 2.0f;


    private void Awake()
    {
        transform.position = Target.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * dumpSpeed);
    }

}
