using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildData : MonoBehaviour
{
    public List<Bounds> buildingColliders;

    private void Start()
    {
        for (int i = 1; transform.GetChild(i) != null; i++)
        {
            buildingColliders.Add(transform.GetChild(i).GetComponent<Collider>().bounds);
        }
    }
}