using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_WayPointControll : MonoBehaviour
{
    public Transform[] Z_WayPoint;
    public int[] WayMovePointNumbers;
    public float Damping;

    private float Way_Distance = 0;
    private int WayPointMaxCount;
    private int wayIndex = 0;

    public Zombies_Position _Position;

    public bool b_Moving = false;
    private void Awake()
    {
        WayPointMaxCount = WayMovePointNumbers.Length;
        
        transform.position = Z_WayPoint[WayMovePointNumbers[0]].position;
        transform.LookAt(Z_WayPoint[WayMovePointNumbers[++wayIndex % WayPointMaxCount]]);

        _Position.Defaultpos();

    }

    Quaternion rot = new Quaternion();
    Vector3 dir = new Vector3();
    private void Update()
    {
        if (!b_Moving) return;

        if (Vector3.Distance(transform.position, Z_WayPoint[WayMovePointNumbers[wayIndex % WayPointMaxCount]].position) <= 0.6f)
        {
            ++wayIndex;
            //transform.LookAt(Z_WayPoint[WayMovePointNumbers[wayIndex % WayPointMaxCount]]);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * Damping);

        dir = (Z_WayPoint[WayMovePointNumbers[wayIndex % WayPointMaxCount]].position - transform.position).normalized;
        rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * Damping);
    }

}
