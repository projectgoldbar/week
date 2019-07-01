using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    MonsterModelChange modelChange;
    public SkinnedMeshRenderer meshRenderer;

    

    private void Awake()
    {
        modelChange = FindObjectOfType<MonsterModelChange>();
        meshRenderer.sharedMesh = modelChange.ChangeModels[Random.Range(0, modelChange.ChangeModels.Length)].sharedMesh;

    }

}
