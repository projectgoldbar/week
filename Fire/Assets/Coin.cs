﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Vector3 RotationSpeed = new Vector3(0f, 0, 100f);

    private ParticlePool particlePool;

    private void Awake()
    {
        particlePool = FindObjectOfType<ParticlePool>();
    }

    /// <summary>
    /// Makes the object rotate on its center every frame.
    /// </summary>
    protected virtual void Update()
    {
        transform.Rotate(RotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnDisable()
    {
        var a = particlePool.GetParticle(particlePool.coinParticlePool);
        a.transform.position = transform.position;
        a.SetActive(true);
    }
}