using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public List<GameObject> particles;
    public List<GameObject> coinParticlePool;

    private void Start()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                coinParticlePool.Add(Instantiate(particles[0], transform.position, Quaternion.identity));
            }
        }
    }

    public GameObject GetParticle(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                return pool[i];
            }
        }
        var a = Instantiate(pool[0], transform.position, Quaternion.identity);
        a.SetActive(false);
        coinParticlePool.Add(a);
        return a;
    }
}