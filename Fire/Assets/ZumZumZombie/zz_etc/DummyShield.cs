using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyShield : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * 50f);
    }
}