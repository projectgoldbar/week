using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    public List<GameObject> stageGate;
    public Manager manager;
    public ParticlePool particlePool;

    private void Start()
    {
        if (manager.playerData.key > 0)
        {
            KeyStageOpen(manager.playerData.key);
        }
    }

    public void InstanceStageOpen(int stage)
    {
        DestroyGate(stage);
    }

    public void KeyStageOpen(int keyCount)
    {
        StartCoroutine(DestroyGate(keyCount));
    }

    private IEnumerator DestroyGate(int time = 0)
    {
        for (int i = 0; i < stageGate.Count; i++)
        {
            if (stageGate[i].activeSelf)
            {
                var particle = particlePool.GetParticle(particlePool.gateOpenParticlePool);
                particle.transform.position = stageGate[i].transform.position;
                particle.SetActive(true);
                stageGate[i].SetActive(false);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}