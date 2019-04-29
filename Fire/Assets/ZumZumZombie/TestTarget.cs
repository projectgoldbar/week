using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
    public Vector3 target;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.3f);

    private void Start()
    {
        StartCoroutine(SetTarget());
    }

    public IEnumerator SetTarget()
    {
        RaycastHit hit;
        for (; ; )
        {
            if (Physics.Raycast(transform.position + transform.forward * -1, transform.forward, out hit, 4f, 1 << 11))
            {
                //Debug.Log("레이히트");
                target = transform.position;
                //Debug.Log(target);
            }
            target = transform.position + transform.forward * 3;
            yield return waitForSeconds;
        }
    }
}