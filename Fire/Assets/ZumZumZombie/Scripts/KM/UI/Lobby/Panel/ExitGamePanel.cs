﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGamePanel : MonoBehaviour
{
    private Vector2 closePos = new Vector2(3500f, 2000f);
    private Vector2 openePos = new Vector2(0f, 0f);

    private void Start()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }

    public void OpenPanel()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = openePos;
    }

    public void ClosePanel()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = closePos;
    }

    public void ExitGame()
    {
        Debug.Log("게임 나가기");
        //게임 나가기 연결
    }
}