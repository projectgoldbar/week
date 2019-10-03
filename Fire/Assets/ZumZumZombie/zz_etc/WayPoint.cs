using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Transform[] waypoints;

    private void Awake()
    {
        waypoints = GetComponentsInChildren<Transform>();
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
}