using System.Collections;
using UnityEngine;

public class Compute : MonoBehaviour
{
    private PlayerData p;

    private void Start()
    {
        p = FindObjectOfType<PlayerData>();
        if (p.maxhp > 200f)
        {
            StartCoroutine(Cd());
        }
    }

    private IEnumerator Cd()
    {
        yield return new WaitForSeconds(30f);
        while (true)
        {
        }
    }
}