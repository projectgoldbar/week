using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Fance : Interaction
{
    public override void Somthing()
    {
        StartCoroutine(Zmove());
        gameObject.layer = LayerMask.NameToLayer("UsedInteraction");
    }

    private IEnumerator Zmove()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.Translate(0, 0, 1f);
            yield return null;
        }
        yield break;
    }
}