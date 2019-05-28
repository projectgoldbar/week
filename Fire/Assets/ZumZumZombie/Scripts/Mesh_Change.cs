using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_Change : MonoBehaviour
{
    public SkinnedMeshRenderer playerMesh;

    public List<Material> materials = new List<Material>();
    public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

    private void Start()
    {
        StartCoroutine(meshChange());
    }

    // Update is called once per frame

    private IEnumerator meshChange()
    {
        for (int j = 0; j < 10; j++) { 
            for (int i = 0; i < skinnedMeshRenderers.Count; i++)
            {
                yield return new WaitForSeconds(0.5f);
                playerMesh.sharedMesh = skinnedMeshRenderers[i].sharedMesh;
                playerMesh.material = materials[i];

               
            }
            yield return null;
        }
    }
}