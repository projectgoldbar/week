﻿using UnityEngine;

public class Coin : MonoBehaviour
{
    public Vector3 RotationSpeed = new Vector3(0f, 0, 100f);
    public int coinSection;
    private ParticlePool particlePool;
    private SectorManager sectorManager;
    public bool coinRotate = true;

    private void Awake()
    {
        particlePool = FindObjectOfType<ParticlePool>();
        sectorManager = FindObjectOfType<SectorManager>();
    }

    private void OnEnable()
    {
        sectorManager.sectors[coinSection].currentCoin++;
        coinRotate = true;
    }

    protected virtual void Update()
    {
        if (!coinRotate)
        {
            return;
        }
        transform.Rotate(RotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnDisable()
    {
        var a = particlePool.GetParticle(particlePool.coinParticlePool);
        a.transform.position = transform.position;
        sectorManager.sectors[coinSection].currentCoin--;
        a.SetActive(true);
    }
}