using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class stringContent : MonoBehaviour
{

    public string content;

    public TextMesh textMesh;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DelayText());
        }
    }


    IEnumerator DelayText()
    {
        for (int i = 0; i < content.Length; i++)
        {
            var t = content.Substring(0, i);
            textMesh.text = t;
            
                yield return new WaitForSeconds(0.01f);
        }
    }

}
