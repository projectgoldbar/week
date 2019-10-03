using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEffectCollider : MonoBehaviour
{
    public bool is_Building = false;

    public void OnTriggerEnter(Collider other)
    {
        is_Building = true;
    }

    private void OnDisable()
    {
        is_Building = false;
    }
}
