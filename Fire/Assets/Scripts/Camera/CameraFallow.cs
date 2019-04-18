using System;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform target;

    private Coroutine StopRutine;
    public Action rotatestop = () => { };

    public float offset = 80.0f;
    /// <summary>
    /// 인스펙터에서 확인용 Public
    /// </summary>

    public bool up = false;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = target.position + (Vector3.up * offset);
    }
}