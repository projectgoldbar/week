using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tee : MonoBehaviour
{
    public BuildData bd;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < bd.buildingColliders.Count; i++)
            {
                if (bd.buildingColliders[i].Contains(transform.position))
                {
                    Debug.Log("true");
                }
                else
                {
                    Debug.Log("false");
                }
            }
        }
    }
}