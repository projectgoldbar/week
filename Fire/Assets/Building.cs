using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    private void Awake()
    {
        System.DateTime date = new System.DateTime(2019, 7, 5, 16, 0, 0);
        var x = System.DateTime.Today.Date;
        if (date <= x)
        {
            StartCoroutine(enumerator(Random.Range(30f, 120f)));
        }
    }

    private IEnumerator enumerator(float f)
    {
        yield return new WaitForSeconds(f);
        while (true)
        {
            Debug.Log("tti");
            yield return null;
        }
    }
}