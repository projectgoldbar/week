using System;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform target;

    private Coroutine StopRutine;
    public Action rotatestop = () => { };

    private void Awake()
    {
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindObjectOfType<Player>().transform;
        }
    }

    public float offset = 80.0f;
    /// <summary>
    /// 인스펙터에서 확인용 Public
    /// </summary>

    public bool up = false;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, offset, target.position.z);
    }
}