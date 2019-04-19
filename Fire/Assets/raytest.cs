using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raytest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    private bool SomethingOnPlace(Vector3 point)
    {
        point = transform.position;
        point.y = 80f;
        Vector3 dir = new Vector3(point.x, point.y - 160, point.z) - point;
        LayerMask layer = LayerMask.NameToLayer("Monster");

        Debug.DrawRay(point, dir, Color.red);
        Debug.DrawRay(point, new Vector3(point.x - 1f, point.y - 80f, point.z) - point);
        Debug.DrawRay(point, new Vector3(point.x, point.y - 80f, point.z + 1f) - point);
        Debug.DrawRay(point, new Vector3(point.x, point.y - 80f, point.z - 1f) - point);
        Debug.DrawRay(point, new Vector3(point.x + 1f, point.y - 80f, point.z) - point);

        if (!Physics.Raycast(point, dir, 200f, 1 << 11))
        {
            return
            Physics.Raycast(point, new Vector3(point.x - 1f, point.y - 80f, point.z) - point, 200f, 1 << 11) &&
            Physics.Raycast(point, new Vector3(point.x, point.y - 80f, point.z + 1f) - point, 200f, 1 << 11) &&
            Physics.Raycast(point, new Vector3(point.x, point.y - 80f, point.z - 1f) - point, 200f, 1 << 11) &&
            Physics.Raycast(point, new Vector3(point.x + 1f, point.y - 80f, point.z) - point, 200f, 1 << 11);
        }
        else return true;
    }

    // Update is called once per frame
    private void Update()
    {
        SomethingOnPlace(transform.position);
    }
}