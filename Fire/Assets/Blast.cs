using System.Collections;
using UnityEngine;

public class Blast : MonoBehaviour
{
    private void OnEnable()
    {
        var a = Physics.OverlapSphere(transform.position, 20f);
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].gameObject.layer == LayerMask.NameToLayer("Magnet"))
            {
                continue;
            }
            else if (a[i].gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                a[i].GetComponent<PlayerData>().Hp = -50f;
            }
            StartCoroutine(KuckBack(a[i]));
        }
    }

    private IEnumerator KuckBack(Collider a)
    {
        for (int k = 0; k < 45; k++)
        {
            a.transform.position = Vector3.MoveTowards(a.transform.position, transform.position, -45f * Time.deltaTime);
            yield return null;
        }

        yield break;
    }
}