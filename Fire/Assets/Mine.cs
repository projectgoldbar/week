using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private ParticlePool particlePool;
    private WaitForSeconds second;
    private WaitForSeconds tracerDuration;
    private Material material;
    private bool isPlayer = false;
    private Coroutine c;

    private void Awake()
    {
        particlePool = FindObjectOfType<ParticlePool>();
        second = new WaitForSeconds(1f);
        tracerDuration = new WaitForSeconds(0.2f);
        material = GetComponent<Renderer>().materials[0];
        c = StartCoroutine(MineBlastSeq());
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayer = true;
        StartCoroutine(MineBlastSeq());
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayer = false;
        StopCoroutine(c);
    }

    private IEnumerator MineBlastSeq()
    {
        StartCoroutine(Tracer());
        int count = 2;
        while (isPlayer)
        {
            yield return second;
            count--;
            if (count < 0)
            {
                isPlayer = false;
                var x = particlePool.GetParticle(particlePool.mineParticlePool);
                x.transform.position = transform.position;
                x.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator Tracer()
    {
        Color color = new Color(207f, 0f, 24f);
        for (int c = 0; c < 4; c++)
        {
            float i = 1;
            material.SetColor("_EmissionColor", Color.green * i);
            yield return tracerDuration;
            i = 1;
            material.SetColor("_EmissionColor", Color.red * i);
            yield return tracerDuration;
        }
        for (int c = 0; c < 4; c++)
        {
            float i = 1;
            material.SetColor("_EmissionColor", Color.red * i);
            yield return tracerDuration;
            i = 1;
            material.SetColor("_EmissionColor", color * i);
            yield return tracerDuration;
        }
        material.SetColor("_EmissionColor", Color.red * 0);
    }
}