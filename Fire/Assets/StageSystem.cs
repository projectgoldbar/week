using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    public List<GameObject> stageGate;
    public List<Stage> stages;
    public Manager manager;
    public ParticlePool particlePool;
    public GameObject arrowPivot;
    private int currentStage = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(DestroyGate());
        }
    }

    public int CurrentStage
    {
        get
        {
            return currentStage;
        }
        set
        {
            //arrowPivot.GetComponent<ArrowMove>().target = stageGate[currentStage].transform;
            if (!arrowPivot.activeSelf)
            {
                arrowPivot.SetActive(true);
            }
        }
    }

    public void InstanceStageOpen()
    {
        StartCoroutine(DestroyGate(currentStage));
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
                var particle = particlePool.GetParticle(particlePool.zombieDamageParticlePool);
                particle.transform.position = stageGate[i].transform.position;
                particle.SetActive(true);
                stageGate[i].SetActive(false);
                stages[i].Setting();
                CurrentStage++;
                yield return new WaitForSeconds(2f);
            }
        }
    }
}