using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpl : MonoBehaviour
{
    public GameObject pivot;

    private void OnDisable()
    {
        pivot.SetActive(false);
    }
}