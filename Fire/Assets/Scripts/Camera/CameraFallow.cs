using System;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset = Vector3.zero;

    private Coroutine StopRutine;
    public Action rotatestop = () => { };

    /// <summary>
    /// 인스펙터에서 확인용 Public
    /// </summary>

    private void OnEnable()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var Move_vec = new Vector3(
            target.localPosition.x,
            target.localPosition.y + offset.y,
            target.localPosition.z);

        transform.position = Move_vec;
    }
}