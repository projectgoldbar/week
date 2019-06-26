using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitUI : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Off", 0.5f);
    }

    private void Off()
    {
        gameObject.SetActive(false);
    }
}