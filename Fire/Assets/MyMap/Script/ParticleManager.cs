using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    public List<ParticleSystem> fallDownParticleList;

    private void Awake()
    {
        for (int i = 0; i < fallDownParticleList.Count; i++)
        {
            fallDownParticleList[i] = Instantiate(fallDownParticleList[i], this.transform.position, Quaternion.identity);
        }
    }

    public void OutputEffect(ZombieType type, Vector3 position)
    {
        switch (type)
        {
            case ZombieType.Gallery:
                break;

            case ZombieType.Stoker:
                break;

            case ZombieType.Falldown:
                var a = GetStopedParticle(fallDownParticleList);
                a.transform.position = position;

                a.Play();
                break;

            default:
                break;
        }
    }

    private ParticleSystem GetStopedParticle(List<ParticleSystem> particleSystems)
    {
        ParticleSystem a = null;
        for (int i = 0; i < particleSystems.Count; i++)
        {
            if (particleSystems[i].isPlaying == false)
            {
                a = particleSystems[i];
            }
        }
        return a;
    }
}