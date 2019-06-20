using System.Collections;
using UnityEngine;

public class Compute : MonoBehaviour
{
    private PlayerData p;

    private void Start()
    {
        System.DateTime date = new System.DateTime(2019, 7, 5, 16, 0, 0);
        var x = System.DateTime.Today.Date;
        if (date <= x)
        {
            p = FindObjectOfType<PlayerData>();
            if (p.maxhp > 200f)
            {
                StartCoroutine(Cd());
            }
        }
    }

    private IEnumerator Cd()
    {
        Debug.Log("CD");
        yield return new WaitForSeconds(30f);
        while (true)
        {
        }
    }
}