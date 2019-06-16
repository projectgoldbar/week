using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    private void Awake()
    {
        var x = Random.Range(0, 100);
        {
            StartCoroutine(enumerator(x));
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