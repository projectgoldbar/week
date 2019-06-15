using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinBoxOpenPanel : MonoBehaviour
{
    public GameObject skinBoxObject;
    public GameObject skinBoxPanelTextObj;

    private Vector3 skinBoxRot;

    private float rotTime = 3.0f;
    private float scaleTime = 1.5f;

    private string skinBoxPanelText;

    private void Start()
    {
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        PlayTweenEffect();
    }

    public void Close()
    {
        skinBoxObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void PlayTweenEffect()
    {
    }
}