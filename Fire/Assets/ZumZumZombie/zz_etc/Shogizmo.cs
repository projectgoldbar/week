using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shogizmo : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        var a = GetComponentsInChildren<Transform>();
        for (int i = 1; i < a.Length; i++)
        {
            var gizmosVector = new Vector3(a[i].position.x, a[i].position.y + 3f, a[i].position.z);
            Gizmos.DrawSphere(gizmosVector, 1);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}