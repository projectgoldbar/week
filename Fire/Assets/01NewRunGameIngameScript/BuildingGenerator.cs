using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    private const int StageTipSize = 30;

    private int currentTipIndex;

    public Transform character;
    public GameObject[] stagetips;
    public int startTipIndex;
    public int preInstantiate;
    public List<GameObject> generatedStageList = new List<GameObject>();

    private void Start()
    {
        currentTipIndex = startTipIndex - 1;
    }

    private void Update()
    {
        int charaPositionIndex = (int)(character.position.z / StageTipSize);

        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    private void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            generatedStageList.Add(stageObject);
        }

        while (generatedStageList.Count > preInstantiate + 2)
        {
            DestroyOldestStage();
        }
        currentTipIndex = toTipIndex;
    }

    private GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stagetips.Length);

        GameObject stageObject = (GameObject)Instantiate(stagetips[nextStageTip], new Vector3(0, 0, tipIndex * StageTipSize), Quaternion.identity);
        return stageObject;
    }

    private void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}