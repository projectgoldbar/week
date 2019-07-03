using System;
using UnityEngine;

public class SturnCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public Action SturnEvent = () => { };

    private void OnEnable()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("박음");

        SturnEvent?.Invoke();
    }
}