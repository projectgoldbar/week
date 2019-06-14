using System.Collections;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float damage = 50f;
    public int kuckBackDistance = 45;

    public float Distance = 20f;

    private LayerMask layerMask;

    private void Awake()
    {
        layerMask = (LayerMask.GetMask("Player")) | (LayerMask.GetMask("Monster"));
    }

    private void OnEnable()
    {

        var a = Physics.OverlapSphere(transform.position, 20f , layerMask);
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].gameObject.layer == LayerMask.NameToLayer("Magnet") || a[i].gameObject.layer == LayerMask.NameToLayer("Sector"))
            {
                continue;
            }
            else if (a[i].gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                a[i].GetComponent<PlayerData>().Hp -= damage;
            }
            StartCoroutine(KuckBack(a[i]));
        }
    }


    private IEnumerator KuckBack(Collider a)
    {
        for (int k = 0; k < kuckBackDistance; k++)
        {
            a.transform.position = Vector3.MoveTowards(a.transform.position, transform.position, -45f * Time.deltaTime);
            yield return null;
        }

        yield break;
    }
}