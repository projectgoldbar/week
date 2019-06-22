﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameScene : MonoBehaviour
{
    public float duration = 3.5f;
    private float toIn = 0.0f;
    private float toOut = 255.0f;

    public GameObject fadeOutImageObj;
    public bool isCompletedFadeOut = false;

    private Color beforeFAdeOutColor;
    private Color nowColor;

    public LobbyBase_Controller lobbyBase_Controller;

    private void resetFadeOut()
    {
        beforeFAdeOutColor.a = toIn;
        fadeOutImageObj.GetComponent<Image>().color = beforeFAdeOutColor;
    }

    private void StartFadeOut()
    {
        isCompletedFadeOut = false;
        var d = LeanTween.value(toIn, toOut, duration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeEffectTweenComplete);
    }

    private void ValueUpdateFade(float value)
    {
        nowColor.a = value / 255f;
        fadeOutImageObj.GetComponent<Image>().color = nowColor;
    }

    private void FadeEffectTweenComplete()
    {
        isCompletedFadeOut = true;
    }

    public void StartGame_Button()
    {
        if (LeanTween.isTweening(lobbyBase_Controller.lobbyPlayerController.gameObject))
        {
            Debug.Log("tweening  fadeOutImageObj//Don't reStart");
            return;
        }
        resetFadeOut();
        StartFadeOut();
        lobbyBase_Controller.lobbyPlayerController.RunningAnim();
    }
}