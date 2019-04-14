using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffectPool : MonoBehaviour
{
    public ParticleSystem blood_effect;

    public Transform parents;

    public int effectCount = 10;

    public List<ParticleSystem> effects = new List<ParticleSystem>();

    public void OnEnable()
    {
        CreateEffect();
    }

    public void CreateEffect()
    {
        for (int i = 0; i < effectCount; i++)
        {
            var go = Instantiate(blood_effect, parents);
            go.Stop();
            go.time = 0;
            effects.Add(go);
        }
    }

    public ParticleSystem Geteffect()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (!effects[i].isPlaying)
            {
                return effects[i];
            }
        }

        return null;
    }
}