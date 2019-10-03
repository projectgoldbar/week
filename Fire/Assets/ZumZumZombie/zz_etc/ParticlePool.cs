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
    public List<GameObject> megidoParticlePool;
    public List<GameObject> zombieDamageParticlePool;
    public List<GameObject> mineParticlePool;
    public List<GameObject> zombieDustParticle;

    #endregion 파티클 생산해서 넣을 풀들

    /*
    파티클순서
    0. 코인
    1. 폭발
    2. 주인공데미지
    3. 고기먹었을때
    4. 폭탄좀비폭발
    5. 클리어 기모으는 효과
    6. 게이트오픈
    7. 지뢰
      */

    private void Start()
    {
        for (int j = 0; j < 20; j++)
        {
            coinParticlePool.Add(Instantiate(particles[0], transform.position, Quaternion.identity, transform));
        }
        //for (int k = 0; k < 5; k++)
        //{
        //    blastParticlePool.Add(Instantiate(particles[1], transform.position, Quaternion.identity, transform));
        //}
        for (int l = 0; l < 5; l++)
        {
            hitParticlePool.Add(Instantiate(particles[2], transform.position, Quaternion.identity, transform));
        }
        //for (int m = 0; m < 3; m++)
        //{
        //    meatParticlePool.Add(Instantiate(particles[3], transform.position, Quaternion.identity, transform));
        //}
        //for (int o = 0; o < 3; o++)
        //{
        //    nukeParticlePool.Add(Instantiate(particles[4], transform.position, Quaternion.identity, transform));
        //}
        for (int i = 0; i < 1; i++)
        {
            megidoParticlePool.Add(Instantiate(particles[5], transform.position, Quaternion.identity, transform));
        }
        for (int q = 0; q < 20; q++)
        {
            zombieDamageParticlePool.Add(Instantiate(particles[6], transform.position, Quaternion.identity, transform));
        }

        for (int i = 0; i < 30; i++)
        {
            zombieDustParticle.Add(Instantiate(particles[8], transform.position, Quaternion.identity, transform));
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
        pool.Add(a);
        return a;
    }
}