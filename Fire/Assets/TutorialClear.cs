using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClear : MonoBehaviour
{
    public StageManager stageManager;
    // Start is called before the first frame update
    private void OnEnable()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        stageManager.LvUp();
    }


    public IEnumerator TutorialZombieClear(Collider[] targets)
    {
        WaitForSeconds seconds = new WaitForSeconds(0.1f);
        Debug.Log($"좀비 몇? {targets.Length}마리");
        for (int i = 0; i < targets.Length; i++)
        {
            Debug.Log(targets[i].name);
            var mat = targets[i].gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
            if (mat == null) Debug.Log("메터리얼 못찾았다.");
            else 
                mat.color = Color.black;

            yield return seconds;
        }
        for (int i = 0; i < targets.Length; i++)
        {
            var dust = stageManager.particlePool.GetParticle(stageManager.particlePool.zombieDustParticle);
            dust.transform.position = targets[i].transform.position;
            dust.transform.rotation = Quaternion.LookRotation(Vector3.up);
            dust.SetActive(true);
            targets[i].gameObject.SetActive(false);
            targets[i].gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.white;
            yield return null;
        }
        for (int i = 0; i < 15; i++)
        {
            yield return seconds;
        }
        stageManager.manager.Evolution();
    }


}
