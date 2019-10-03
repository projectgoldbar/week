using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform pivot;
    public int zombieCount = 3;

    public virtual void Use()
    {
        StartCoroutine(Interactioncoroutine());

        gameObject.layer = LayerMask.NameToLayer("UsedInteraction");
    }

    private IEnumerator Interactioncoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            //pivot.rotation *= Quaternion.Euler(Vector3.left * 30f * Time.deltaTime);
            pivot.Rotate(Vector3.right * -9f);
            yield return null;
        }
    }
}