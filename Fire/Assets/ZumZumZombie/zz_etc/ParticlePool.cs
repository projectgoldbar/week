using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    //파티클종류
    public List<GameObject> particles;

    #region 파티클 생산해서 넣을 풀들

    public List<GameObject> coinParticlePool;
    public List<GameObject> blastParticlePool;
    public List<GameObject> hitParticlePool;
    public List<GameObject> meatParticlePool;
    public List<GameObject> nukeParticlePool;
    public List<GameObject> trapParticlePool;

    #endregion 파티클 생산해서 넣을 풀들

    /*
    파티클순서
    0. 코인
    1. 폭발
    2. 주인공데미지
    3. 고기먹었을때
    4. 폭탄좀비폭발
    5. 함정
      */

    private void Start()
    {
        for (int j = 0; j < 20; j++)
        {
            coinParticlePool.Add(Instantiate(particles[0], transform.position, Quaternion.identity, transform));
        }
        for (int k = 0; k < 5; k++)
        {
            blastParticlePool.Add(Instantiate(particles[1], transform.position, Quaternion.identity, transform));
        }
        for (int l = 0; l < 20; l++)
        {
            hitParticlePool.Add(Instantiate(particles[2], transform.position, Quaternion.identity, transform));
        }
        for (int m = 0; m < 5; m++)
        {
            meatParticlePool.Add(Instantiate(particles[3], transform.position, Quaternion.identity, transform));
        }
        for (int o = 0; o < 3; o++)
        {
            nukeParticlePool.Add(Instantiate(particles[4], transform.position, Quaternion.identity, transform));
        }
        for (int p = 0; p < 4; p++)
        {
            trapParticlePool.Add(Instantiate(particles[5], transform.position, Quaternion.identity, transform));
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
        var a = Instantiate(pool[0], transform.position, Quaternion.identity, transform);
        a.SetActive(false);
        coinParticlePool.Add(a);
        return a;
    }
}