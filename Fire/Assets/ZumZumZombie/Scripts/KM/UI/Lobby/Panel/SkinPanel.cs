﻿using UnityEngine;

public class SkinPanel : MonoBehaviour
{
    private Vector2 closePos = new Vector2(0f, 1000f);
    private Vector2 openePos = new Vector2(0f, 0f);

    private void Start()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<RectTransform>().anchoredPosition = openePos;
    }

    public void ClosePanel()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
        gameObject.SetActive(false);
    }
}