using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameScene : MonoBehaviour
{
    public float duration = 4.0f;
    private float toIn = 0.0f;
    private float toOut = 255.0f;

    public GameObject fadeOutImageObj;
    public bool isCompletedFadeOut = false;

    private Color beforeFAdeOutColor;
    private Color nowColor;

    public LobbyBaseFlowManager lobbyBaseFlowManager;

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
        resetFadeOut();
        StartFadeOut();
        lobbyBaseFlowManager.lobbyPlayerController.RunningAnim();
    }
}