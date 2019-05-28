using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Fall : Interaction
{
    public override void Use()
    {
        StartCoroutine(Fallen());
    }

    private IEnumerator Fallen()
    {
        float z = 0.1f;
        for (int i = 0; i < 100; i++)
        {
            transform.Rotate(0, 0, z);
            z += 0.01f;
            yield return null;
        }
        yield break;
    }
}