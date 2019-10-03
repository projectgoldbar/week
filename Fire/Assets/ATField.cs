using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATField : MonoBehaviour
{
    public Image image;
        WaitForSeconds seconds ;

    private void Awake()
    {
        seconds = new WaitForSeconds(0.1f);
    }

    private void OnEnable()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        for (int i = 0; i < 9; i++)
        {
            if (image.enabled)
            {
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
            }
            yield return seconds;
        }
        gameObject.SetActive(false);
        yield break;
    }
}