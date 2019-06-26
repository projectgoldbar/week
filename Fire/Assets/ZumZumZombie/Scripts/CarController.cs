using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarController : Move
{
    private float eulerDirY = 0;

    private new Rigidbody rid;
    private float currentY = 0;

    public float CarMoveSpeed = 500.0f;

    public new void Awake()
    {
        rid = GetComponent<Rigidbody>();
    }

    public override void Car_LeftTurn()
    {
        eulerDirY = transform.rotation.eulerAngles.y;

        currentY = eulerDirY - 90.0f;

        Quaternion rot = Quaternion.Euler(0, currentY, 0);

        transform.rotation = rot;
    }

    public override void Car_RightTurn()
    {
        eulerDirY = transform.rotation.eulerAngles.y;

        currentY = eulerDirY + 90.0f;
        Quaternion rot = Quaternion.Euler(0, currentY, 0);
        transform.rotation = rot;
    }

    public override void SelectState()
    {
        rid.velocity = rid.transform.forward * CarMoveSpeed;
    }

    private IEnumerator SmoothRotation(Quaternion rot)
    {
        yield return null;
    }
}