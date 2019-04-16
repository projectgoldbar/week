using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Move
{
    public override void MoveState()
    {
    }

    public override void Car_LeftTurn()
    {
        Debug.Log("좌");
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90.0f, 0);
    }

    public override void Car_RightTurn()
    {
        Debug.Log("우");
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90.0f, 0);
    }
}